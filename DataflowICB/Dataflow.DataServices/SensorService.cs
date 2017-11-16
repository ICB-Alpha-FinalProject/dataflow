using Bytes2you.Validation;
using Dataflow.DataServices.Contracts;
using Dataflow.DataServices.Models;
using Dataflow.Services.Contracts;
using DataflowICB.App_Start.Contracts;
using DataflowICB.App_Start.Models;
using DataflowICB.Database;
using DataflowICB.Database.Models;
using DataflowICB.Models.DataApi;
using Newtonsoft.Json;
using SensorApiModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dataflow.DataServices
{
    public class SensorService : ISensorService
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpClientProvider httpClient;
        private readonly IEmailService emailService;
        private bool valueHasAlreadyBeenChangedForThisInvalidValue;

        public SensorService(ApplicationDbContext context, IHttpClientProvider httpClient, IEmailService emailService)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();
            Guard.WhenArgument(httpClient, "httpClient").IsNull().Throw();
            Guard.WhenArgument(emailService, "emailService").IsNull().Throw();

            this.context = context;
            this.httpClient = httpClient;
            this.emailService = emailService;
        }

        public void AddSensor(Sensor sensor)
        {
            Guard.WhenArgument(sensor, "sensor").IsNull().Throw();

            this.context.Sensors.Add(sensor);
            this.context.SaveChanges();
        }

        public void EditSensor(ISensorDataModel editedSensor)
        {
            Guard.WhenArgument(editedSensor, "editedSensor").IsNull().Throw();

            var sensor = this.context.Sensors.First(s => s.Id == editedSensor.Id);
            if (sensor != null)
            {
                sensor.Name = editedSensor.Name;
                sensor.Description = editedSensor.Description;
                sensor.URL = editedSensor.URL;
                sensor.PollingInterval = editedSensor.PollingInterval;
                sensor.IsBoolType = editedSensor.IsBoolType;
                sensor.IsPublic = editedSensor.IsPublic;
                sensor.IsShared = editedSensor.IsShared;
                sensor.IsDeleted = editedSensor.IsDeleted;

                if (sensor.IsBoolType == true)
                {
                    if (sensor.BoolTypeSensor == null)
                    {
                        sensor.BoolTypeSensor = new BoolTypeSensor();
                    }

                    sensor.BoolTypeSensor.MeasurementType = editedSensor.MeasurementType;
                    sensor.BoolTypeSensor.CurrentValue = true;
                }

                else
                {
                    if (sensor.ValueTypeSensor == null)
                    {
                        sensor.ValueTypeSensor = new ValueTypeSensor();
                    }

                    sensor.ValueTypeSensor.MeasurementType = editedSensor.MeasurementType;
                    sensor.ValueTypeSensor.CurrentValue = 0.0;
                }
               
                    this.context.SaveChanges();
              
            }
        }

        public IEnumerable<SensorDataModel> GetAllPublicSensors()
        {
            var publicSenors = this.context.Sensors.Where(m => m.IsPublic == true && m.IsDeleted == false)
                .Select(m => new SensorDataModel
                {
                    Id = m.Id,
                    Owner = m.Owner.UserName,
                    Name = m.Name,
                    Description = m.Description,
                    MeasurementType = m.IsBoolType ? m.BoolTypeSensor.MeasurementType : m.ValueTypeSensor.MeasurementType,
                    CurrentValue = m.IsBoolType ? m.BoolTypeSensor.CurrentValue.ToString() : m.ValueTypeSensor.CurrentValue.ToString(),
                    IsBoolType = m.IsBoolType,
                    MinValue = m.IsBoolType ? 0 : m.ValueTypeSensor.MinValue,
                    MaxValue = m.IsBoolType ? 0 : m.ValueTypeSensor.Maxvalue

                }).ToList();

            return publicSenors;
        }

        public IEnumerable<SensorDataModel> GetAllSensors(bool IsAdmin)
        {
            if (IsAdmin == true)
            {
                var allSensors = this.context.Sensors.Select(SensorDataModel.Create)
                .ToList();

                return allSensors;
            }

            else
            {
                var allSensors = this.context.Sensors
                .Where(s => s.IsDeleted == false).Select(SensorDataModel.Create)
                .ToList();

                return allSensors;
            }
        }

        public SensorDataModel GetAdminSensorById(int Id)
        {
            Sensor sensorModel = this.context.Sensors.First(s => s.Id == Id);
            SensorDataModel sensor = SensorDataModel.Convert(sensorModel);

            return sensor;
        }

        public SensorDataModel GetUserSensorById(int id)
        {

            var sensor = this.context.Sensors.Where(s => s.Id == id && s.IsDeleted == false)
                .Select(m => new SensorDataModel
                {
                    Id = m.Id,
                    OwnerId = m.OwnerId,
                    CurrentValue = m.IsBoolType ? m.BoolTypeSensor.CurrentValue.ToString() : m.ValueTypeSensor.CurrentValue.ToString(),
                    Name = m.Name,
                    Description = m.Description,
                    URL = m.URL,
                    PollingInterval = m.PollingInterval,
                    MeasurementType = m.IsBoolType ? m.BoolTypeSensor.MeasurementType : m.ValueTypeSensor.MeasurementType,
                    IsBoolType = m.IsBoolType,
                    IsPublic = m.IsPublic,
                    IsShared = m.IsShared,
                    MaxValue = m.IsBoolType ? 0.0 : m.ValueTypeSensor.Maxvalue,
                    MinValue = m.IsBoolType ? 0.0 : m.ValueTypeSensor.MinValue

                    //kvo stava ne trq da ima max i min a lowset i highest li, mi ednoto trqq da e granicata na senzra a drugto ot inputa ama ...
                    // sec
                }).FirstOrDefault();

            return sensor;
        }

        public async Task UpdateSensors()
        {
            var sensorsForUpdate = this.context.Sensors
                .Where(s => (DbFunctions.AddSeconds(s.LastUpdate, s.PollingInterval) <= DateTime.Now))
                .ToList();

            foreach (Sensor s in sensorsForUpdate)
            {
                var url = s.URL;

                var resp = await httpClient.GetAsync(url);
                var content = await resp.Content.ReadAsStringAsync();

                var updatedValue = JsonConvert.DeserializeObject<SensorApiUpdate>(content);
                int pollingInterval = s.PollingInterval;

                if (s.IsBoolType)
                {
                    if (s.BoolTypeSensor.CurrentValue != bool.Parse(updatedValue.Value))
                    {
                        var valueHistory = new ValueHistory()
                        {
                            BoolSensor = s.BoolTypeSensor,
                            Date = updatedValue.TimeStamp,
                            Value = updatedValue.Value.ToLower() == "true" ? 1 : 0
                        };
                        s.BoolTypeSensor.BoolHistory.Add(valueHistory);
                        s.BoolTypeSensor.CurrentValue = bool.Parse(updatedValue.Value);
                    }
                }
                else
                {
                    if (s.ValueTypeSensor.CurrentValue != double.Parse(updatedValue.Value))
                    {
                        var valueHistory = new ValueHistory()
                        {
                            ValueSensor = s.ValueTypeSensor,
                            Date = updatedValue.TimeStamp,
                            Value = double.Parse(updatedValue.Value)
                        };
                        s.ValueTypeSensor.ValueHistory.Add(valueHistory);
                        s.ValueTypeSensor.CurrentValue = double.Parse(updatedValue.Value);

                        SendEmailIfNeeded(s);
                    }
                }

                s.LastUpdate = updatedValue.TimeStamp;
            }

            this.context.SaveChanges();
        }

        public void SendEmailIfNeeded(Sensor sensor)
        {
            Guard.WhenArgument(sensor, "sensor").IsNull().Throw();

            if ((sensor.ValueTypeSensor.CurrentValue <= sensor.ValueTypeSensor.Maxvalue && sensor.ValueTypeSensor.CurrentValue >= sensor.ValueTypeSensor.MinValue) && valueHasAlreadyBeenChangedForThisInvalidValue == false)
            {
                valueHasAlreadyBeenChangedForThisInvalidValue = false;
            }

            if ((sensor.ValueTypeSensor.CurrentValue > sensor.ValueTypeSensor.Maxvalue || sensor.ValueTypeSensor.CurrentValue < sensor.ValueTypeSensor.MinValue) && valueHasAlreadyBeenChangedForThisInvalidValue == false)
            {
                SendEmailToTheUserOfTheOfflineSensor(sensor);
                valueHasAlreadyBeenChangedForThisInvalidValue = true;
            }
        }

        public void SendEmailToTheUserOfTheOfflineSensor(Sensor sensor)
        {
            Guard.WhenArgument(sensor, "sensor").IsNull().Throw();

            var message = new EmailMessage();
            message.ToEmail = sensor.Owner.Email;
            message.Subject = $"Sensor offline";
            message.IsHtml = true;
            message.Body =
                String.Format($"Sensor {sensor.Name} is offline");

            var status = emailService.SendEmailMessage(message);
        }

        public IEnumerable<SensorDataModel> GetAllSensorsForUser(string username)
        {
            var sensorForUser = context.Sensors.Where(s => s.Owner.UserName == username && s.IsDeleted == false)
                .Select(sensor => new SensorDataModel
                {
                    Id = sensor.Id,
                    Name = sensor.Name,
                    Description = sensor.Description,
                    CurrentValue = sensor.IsBoolType ? sensor.BoolTypeSensor.CurrentValue.ToString() : sensor.ValueTypeSensor.CurrentValue.ToString(),
                    IsBoolType = sensor.IsBoolType,
                    IsPublic = sensor.IsPublic,
                    IsShared = sensor.IsShared,
                    IsConnected = sensor.IsBoolType ? sensor.BoolTypeSensor.IsConnected : sensor.ValueTypeSensor.IsConnected,
                    MeasurementType = sensor.IsBoolType ? sensor.BoolTypeSensor.MeasurementType : sensor.ValueTypeSensor.MeasurementType,
                    MaxValue = sensor.IsBoolType ? 0 : sensor.ValueTypeSensor.Maxvalue,
                    MinValue = sensor.IsBoolType ? 0 : sensor.ValueTypeSensor.MinValue
                })
                .ToList();

            return sensorForUser;
        }

        public void ShareSensorWithUser(int id, string username)
        {
            Guard.WhenArgument(username, "username").IsNull().Throw();

            var sharedSensor = this.context.Sensors.Single(s => s.Id == id && s.IsDeleted == false);

            var user = this.context.Users.Single(n => n.UserName == username);
            sharedSensor.IsShared = true;
            sharedSensor.SharedWithUsers.Add(user);

            this.context.SaveChanges();
        }

        public void DeleteSensor(int id)
        {
            var sensorToDelete = this.context.Sensors.Single(s => s.Id == id);

            sensorToDelete.IsDeleted = true;

            this.context.SaveChanges();
        }

        public SensorDataModel GetUsersSharedSensor(int id)
        {
            var sharedSensor = this.context.Sensors.Single(s => s.Id == id && s.IsDeleted == false).SharedWithUsers.ToList();

            var sensorDModel = new SensorDataModel()
            {
                Id = id,
                
            };

            for (int i = 0; i < sharedSensor.Count; i++)
            {
                sensorDModel.SharedWithUsers.Add(sharedSensor[i].UserName);
                sensorDModel.Owner = sharedSensor[i].UserName;
            }

            return sensorDModel;
        }

        public IEnumerable<SensorDataModel> GetSharedWithUserSensors(string username)
        {
            Guard.WhenArgument(username, "username").IsNull().Throw();

            var userSharedSensors = this.context.Users.First(n => n.UserName == username).SharedSensors
                .Select(sensor => new SensorDataModel()
                {
                    Id = sensor.Id,
                    Name = sensor.Name,
                    Description = sensor.Description,
                    CurrentValue = sensor.IsBoolType ? sensor.BoolTypeSensor.CurrentValue.ToString() : sensor.ValueTypeSensor.CurrentValue.ToString(),
                    IsBoolType = sensor.IsBoolType,
                    IsPublic = sensor.IsPublic,
                    IsShared = sensor.IsShared,
                    IsConnected = sensor.IsBoolType ? sensor.BoolTypeSensor.IsConnected : sensor.ValueTypeSensor.IsConnected,
                    MeasurementType = sensor.IsBoolType ? sensor.BoolTypeSensor.MeasurementType : sensor.ValueTypeSensor.MeasurementType
                }).ToList();

            return userSharedSensors;
        }



        public IEnumerable<SensorApiUpdate> HistoryDataForBoolSensorsById(int sensorId)
        {
            var boolHistoryData = this.context.ValueHistory
                .Where(h => h.BoolSensorId == sensorId)
               .Select(s => new SensorApiUpdate
               {
                   TimeStamp = s.Date,
                   Value = s.Value.ToString()
               })
               .ToList();

            return boolHistoryData;
        }

        public IEnumerable<SensorApiUpdate> HistoryDataForValueSensorsById(int sensorId)
        {
            var valueHistoryData = this.context.ValueHistory
                .Where(h => h.ValueSensorId == sensorId)
               .Select(s => new SensorApiUpdate
               {
                   TimeStamp = s.Date,
                   Value = s.Value.ToString()
               })
               .ToList();

            return valueHistoryData;
        }

        public bool ValueHasAlreadyBeenChangedForThisInvalidValue { get; }
    }
}

