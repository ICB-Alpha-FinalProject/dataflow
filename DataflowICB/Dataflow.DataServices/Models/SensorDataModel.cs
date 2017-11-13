using Dataflow.DataServices.Contracts;
using DataflowICB.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dataflow.DataServices.Models
{
    public class SensorDataModel : ISensorDataModel
    {
        private ICollection<string> sharedWithUsers;

        public SensorDataModel()
        {
            this.sharedWithUsers = new HashSet<string>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string MeasurementType { get; set; }

        public string Description { get; set; }

        public string URL { get; set; }

        public int PollingInterval { get; set; }
        
        public string CurrentValue { get; set; }

        public double MaxValue { get; set; }

        public double MinValue { get; set; }

        public bool IsBoolType { get; set; }

        public bool IsPublic { get; set; }

        public bool IsShared { get; set; }

        public bool IsConnected { get; set; }

        public string OwnerId { get; set; }

        public string Owner { get; set; }

        public ICollection<string> SharedWithUsers
        {
            get
            {
                return this.sharedWithUsers;
            }
            set
            {
                this.sharedWithUsers = value;
            }
        }

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
                    MeasurementType = s.IsBoolType ? s.BoolTypeSensor.MeasurementType : s.ValueTypeSensor.MeasurementType,
                    Description = s.Description,
                    URL = s.URL,
                    PollingInterval = s.PollingInterval,
                    CurrentValue = s.IsBoolType ? s.BoolTypeSensor.CurrentValue.ToString() : s.ValueTypeSensor.CurrentValue.ToString(),
                    MinValue = s.IsBoolType ? 0.0 : s.ValueTypeSensor.MinValue,
                    MaxValue = s.IsBoolType ? 0.0 : s.ValueTypeSensor.Maxvalue,
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
                MeasurementType = sensor.IsBoolType ? sensor.BoolTypeSensor.MeasurementType : sensor.ValueTypeSensor.MeasurementType,
                Description = sensor.Description,
                URL = sensor.URL,
                PollingInterval = sensor.PollingInterval,
                CurrentValue = sensor.IsBoolType ? sensor.BoolTypeSensor.CurrentValue.ToString() : sensor.ValueTypeSensor.CurrentValue.ToString(),
                MinValue = sensor.IsBoolType ? 0.0 : sensor.ValueTypeSensor.MinValue,
                MaxValue = sensor.IsBoolType ? 0.0 : sensor.ValueTypeSensor.Maxvalue,
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
