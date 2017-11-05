using System;

namespace DataflowICB.Database.Models
{
    public class ValueTypeSensor
    {
        public Guid Id { get; set; }

        public string MeasurementType { get; set; }

        public Sensor SensorModel { get; set; }

        public Guid SensorModelId { get; set; }

        public double MinValue { get; set; }

        public double Maxvalue { get; set; }

        public bool? IsInAcceptableRange { get; set; }

        public bool? IsConnected { get; set; }



    }
}