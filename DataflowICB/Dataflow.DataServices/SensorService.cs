using Bytes2you.Validation;
using Dataflow.DataServices.Contracts;
using Dataflow.DataServices.Models;
using DataflowICB.Database;
using DataflowICB.Database.Models;
using DataflowICB.Models.DataApi;
using Newtonsoft.Json;
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
        private readonly HttpClient client;

        public SensorService(ApplicationDbContext context, HttpClient client)
        {
            this.context = context;

            this.client = client;
            this.client.DefaultRequestHeaders.Add("auth-token", "8e4c46fe-5e1d-4382-b7fc-19541f7bf3b0");
        }

        public void AddSensor(Sensor sensor)
        {
            this.context.Sensors.Add(sensor);
            this.context.SaveChanges();

        }

        public void EditSensor(SensorDataModel editedSensor)
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
            var publicSenors = this.context.Sensors.Where(m => m.IsPublic == true)
                .Select(m => new SensorDataModel
                {
                    Id = m.Id,
                    Owner = m.Owner.UserName,
                    Name = m.Name,
                    Description = m.Description

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

        public SensorDataModel GetSensorById(int Id)
        {
            Sensor sensorModel = this.context.Sensors.First(s => s.Id == Id);
            SensorDataModel sensor = SensorDataModel.Convert(sensorModel);

            return sensor;
        }

        public SensorDataModel GetUserSensorById(int id)
        {
            var sensor = this.context.Sensors.Where(s => s.Id == id)
                .Select(m => new SensorDataModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    URL = m.URL,
                    PollingInterval = m.PollingInterval,
                    MeasurementType = m.IsBoolType ? m.BoolTypeSensor.MeasurementType : m.ValueTypeSensor.MeasurementType,
                    IsBoolType = m.IsBoolType,
                    IsPublic = m.IsPublic,
                    IsShared = m.IsShared,
                    MaxValue = m.ValueTypeSensor.Maxvalue,
                    MinValue = m.ValueTypeSensor.MinValue
                }).First();

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

                var content = await client.GetStringAsync(url);
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
                            Value = double.Parse(updatedValue.Value)
                        };
                        s.BoolTypeSensor.BoolHistory.Add(valueHistory);
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
                    }

                }

                s.LastUpdate = updatedValue.TimeStamp;
            }

            this.context.SaveChanges();
        }



        public IEnumerable<SensorDataModel> GetAllSensorsForUser(string username)
        {
            var sensorForUser = context.Sensors.Where(s => s.Owner.UserName == username)
                .Select(sensor => new SensorDataModel
                {
                    Id = sensor.Id,
                    Name = sensor.Name,
                    Description = sensor.Description,
                    CurrentValue = sensor.IsBoolType ? sensor.BoolTypeSensor.CurrentValue.ToString() : sensor.ValueTypeSensor.CurrentValue.ToString(),
                    IsBoolType = sensor.IsBoolType,
                    IsPublic = sensor.IsPublic,
                    IsShared = sensor.IsShared
                })
                .ToList();

            return sensorForUser;
        }

        public SensorDataModel ShareWithUser(string username)
        {
            return new SensorDataModel();
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
    }
}
