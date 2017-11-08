using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataflowICB.Database.Models
{
    public class BoolTypeSensor
    {
        private ICollection<TimeHistory> boolHistory;

        public BoolTypeSensor()
        {
            this.boolHistory = new List<TimeHistory>();
        }

        [Key, ForeignKey("Sensor")]
        public string Id { get; set; }

        [Required]
        public string MeasurementType { get; set; }

        public string SensorId { get; set; }

        public virtual Sensor Sensor { get; set; }

        public bool IsConnected { get; set; }

        [Required]
        public bool CurrentValue { get; set; }

        public virtual ICollection<TimeHistory> BoolHistory
        {
            get
            {
                return this.boolHistory;
            }
            set
            {
                this.boolHistory = value;
            }
        }
    }
}