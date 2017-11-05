using System;
using System.ComponentModel.DataAnnotations;

namespace DataflowICB.Database.Models
{
    public class TimeHistory
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Value { get; set; }

        public ValueTypeSensor ValueSensor { get; set; }

        public string ValueSensorId { get; set; }

        public BoolTypeSensor BoolSensor { get; set; }

        public string BoolSensorId { get; set; }
    }
}