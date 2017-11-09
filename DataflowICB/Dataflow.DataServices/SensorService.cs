﻿using Dataflow.DataServices.Contracts;
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

        public IEnumerable<Sensor> GetAllSensors()
        {
            var allSensors = this.context.Sensors
                .Where(s => s.IsDeleted == false)
                .ToList();

            return allSensors;
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

    }
}
