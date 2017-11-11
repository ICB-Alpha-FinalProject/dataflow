using DataflowICB.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dataflow.DataServices.Models
{
    public class SensorDataModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string URL { get; set; }

        public int PollingInterval { get; set; }
        
        public string CurrentValue { get; set; }
        
        public bool IsBoolType { get; set; }

        public bool IsPublic { get; set; }

        public bool IsShared { get; set; }

        public string OwnerId { get; set; }

        public string Owner { get; set; }

        public ICollection<string> SharedWithUsers { get; set; }

        public double SensorCoordinatesX { get; set; }

        public double SensorCoordinatesY { get; set; }

        public DateTime LastUpdate { get; set; }

        public bool IsDeleted { get; set; }

        public static Expression<Func<Sensor, SensorDataModel>> Create
        {
            get
            {
                return s => new SensorDataModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    URL = s.URL,
                    PollingInterval = s.PollingInterval,
                    CurrentValue = s.IsBoolType ? s.BoolTypeSensor.CurrentValue.ToString() : s.ValueTypeSensor.CurrentValue.ToString(),
                    IsBoolType = s.IsBoolType,
                    IsPublic = s.IsPublic,
                    IsShared = s.IsShared,
                    OwnerId = s.OwnerId,
                    Owner = s.Owner.UserName,
                    SharedWithUsers = s.SharedWithUsers.Select(sh => sh.UserName).ToList(),
                    SensorCoordinatesX = s.SensorCoordinatesX,
                    SensorCoordinatesY = s.SensorCoordinatesY,
                    LastUpdate = s.LastUpdate,
                    IsDeleted = s.IsDeleted
                };
            }
        }

        public static SensorDataModel Convert(Sensor sensor)
        {
            SensorDataModel sensorViewModel = new SensorDataModel()
            {
                Id = sensor.Id,
                Name = sensor.Name,
                Description = sensor.Description,
                URL = sensor.URL,
                PollingInterval = sensor.PollingInterval,
                CurrentValue = sensor.IsBoolType ? sensor.BoolTypeSensor.CurrentValue.ToString() : sensor.ValueTypeSensor.CurrentValue.ToString(),
                IsBoolType = sensor.IsBoolType,
                IsPublic = sensor.IsPublic,
                IsShared = sensor.IsShared,
                OwnerId = sensor.OwnerId,
                Owner = sensor.Owner.UserName,
                SharedWithUsers = sensor.SharedWithUsers.Select(sh => sh.UserName).ToList(),
                SensorCoordinatesX = sensor.SensorCoordinatesX,
                SensorCoordinatesY = sensor.SensorCoordinatesY,
                LastUpdate = sensor.LastUpdate,
                IsDeleted = sensor.IsDeleted
            };

            return sensorViewModel;
        }
    }
}
