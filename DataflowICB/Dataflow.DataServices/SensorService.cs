using Dataflow.DataServices.Contracts;
using DataflowICB.Database;
using DataflowICB.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataflow.DataServices
{
    public class SensorService : ISensorService
    {
        private readonly ApplicationDbContext context;

        public SensorService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddSensor(Sensor sensor)
        {
            this.context.Sensors.Add(sensor);

            try
            {
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
