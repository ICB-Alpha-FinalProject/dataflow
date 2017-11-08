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

        public string ValueSensorId { get; set; }

        public virtual ValueTypeSensor ValueSensor { get; set; }

        public string BoolSensorId { get; set; }

        public virtual BoolTypeSensor BoolSensor { get; set; }
    }
}