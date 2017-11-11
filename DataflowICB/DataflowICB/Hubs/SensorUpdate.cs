using Dataflow.DataServices.Contracts;
using DataflowICB.Models.DataApi;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataflowICB.Hubs
{
    public class SensorUpdate : Hub
    {
        private readonly ISensorService sensorService;

        public SensorUpdate(ISensorService sensorService)
        {
            this.sensorService = sensorService;
        }

        public void Send(SensorApiUpdate newData)
        {
            this.Clients.All.updateChart(newData);
        }
    }
}