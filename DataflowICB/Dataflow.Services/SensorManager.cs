using Dataflow.Services.Contracts;
using DataflowICB.Database.Models;
using DataflowICB.Models.DataApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dataflow.Services
{
    public class SensorManager : ISensorManager
    {
        private readonly HttpClient client;

        public SensorManager(HttpClient client)
        {
            this.client = client;
        }

        public async Task UpdateSensors(IEnumerable<Sensor> sensors)
        {
            foreach (Sensor s in sensors)
            {
                var url = s.URL;
                var content = await client.GetStringAsync(url);

                var updatedValue = JsonConvert.DeserializeObject<SensorApiUpdate>(content);

                int pollingInterval = s.PollingInterval;

                if (s.LastUpdate.AddSeconds(pollingInterval) < updatedValue.TimeStamp)
                {
                    continue;
                }

                if (s.IsBoolType)
                {
                    if (s.BoolTypeSensor.CurrentValue == bool.Parse(updatedValue.Value))
                    {
                        s.LastUpdate = updatedValue.TimeStamp;
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
        }
    }
}
