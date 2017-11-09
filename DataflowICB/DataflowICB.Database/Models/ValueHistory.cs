using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataflowICB.Database.Models
{
    public class ValueHistory
    {
        public ValueHistory()
        {

        }

        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Value { get; set; }

        public int? ValueSensorId { get; set; }

        public virtual ValueTypeSensor ValueSensor { get; set; }

        public int? BoolSensorId { get; set; }

        public virtual BoolTypeSensor BoolSensor { get; set; }
    }
}