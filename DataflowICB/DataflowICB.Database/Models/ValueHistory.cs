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

        public ValueTypeSensor ValueSensor { get; set; }

        public int? BoolSensorId { get; set; }

        public BoolTypeSensor BoolSensor { get; set; }

    }
}