using System;
using System.Collections.Generic;

namespace Dataflow.DataServices.Models
{
    public class SensorDataModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string URL { get; set; }

        public int PollingInterval { get; set; }

        public ValueTypeSensorDataModel ValueTypeSensor { get; set; }

        public BoolTypeSensorDataModel BoolTypeSensor { get; set; }

        public bool IsBoolType { get; set; }

        public bool IsPublic { get; set; }

        public string OwnerId { get; set; }

        public UserDataModel Owner { get; set; }

        public ICollection<UserDataModel> SharedWithUsers { get; set; }

        public double SensorCoordinatesX { get; set; }

        public double SensorCoordinatesY { get; set; }

        public DateTime LastUpdate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
