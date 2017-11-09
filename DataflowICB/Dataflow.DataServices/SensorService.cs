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

        public IEnumerable<SensorServiceModel> GetAllSensorsForUser(string username)
        {
            var sensorForUser = context.Sensors.Where(s => s.Owner.UserName == username)
                .Select(sensor => new SensorServiceModel
                {
                    Name = sensor.Name,
                    Description = sensor.Description,
                    CurrentValue = sensor.IsBoolType ? sensor.BoolTypeSensor.CurrentValue.ToString(): sensor.ValueTypeSensor.CurrentValue.ToString(),
                    IsPublic = sensor.IsPublic,
                    IsShared = sensor.SharedWithUsers.Count() > 0
                    
                })
                .ToList();

            return sensorForUser;
        }

        public SensorServiceModel ShareWithUser(string username)
        {

        }

        
    }
}
