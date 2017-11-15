using Dataflow.DataServices.Contracts;
using DataflowICB.Areas.Sensor.Models;
using DataflowICB.Database;
using DataflowICB.Models.DataApi;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using SensorApiModels.SensorViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataflowICB.Hubs
{
    public class SensorUpdate : Hub
    {
        //private readonly ISensorService sensorService;
        //private readonly ApplicationDbContext context;

        public SensorUpdate(/*ISensorService sensorService, ApplicationDbContext context*/)
        {
            //this.sensorService = sensorService;
            //this.context = context;
        }
        
        public void Send(IEnumerable<int> ids)
        {
            //var sensorsToUpdate = this.context.Sensors.ToList();
            using (var context = new ApplicationDbContext())
            {
                var res = new List<RealTimeSensor>();
                foreach (var id in ids)
                {
                    var found = context.Sensors
                        .Select(s => new RealTimeSensor
                        {
                            Id = s.Id,
                            Value = s.IsBoolType ? s.BoolTypeSensor.CurrentValue.ToString()
                                    : s.ValueTypeSensor.CurrentValue.ToString(),
                            MinValue = s.IsBoolType ? 0 : s.ValueTypeSensor.MinValue,
                            MaxValue = s.IsBoolType ? 0 : s.ValueTypeSensor.Maxvalue,
                            IsValueType = !s.IsBoolType
                        })
                        .First(s => s.Id == id);

                    res.Add(found);

                }
                var jsonList = JsonConvert.SerializeObject(res);
                Clients.All.updateChart(jsonList);
            }
        }
    }
}