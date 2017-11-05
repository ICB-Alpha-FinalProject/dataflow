using System;

namespace DataflowICB.Database.Models
{
    public class BoolTypeSensor
    {
        public string MeasurementType { get; set; }

        public Guid Id { get; set; }

        public Sensor SensorModel { get; set; }

        public Guid SensorModelId { get; set; }

        public bool isInAcceptableRange { get; set; }

        public bool isConnected { get; set; }


    }
}