using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataflowICB.Database.Models
{
    public class BoolTypeSensor
    {
        private ICollection<ValueHistory> boolHistory;

        public BoolTypeSensor()
        {
            this.boolHistory = new List<ValueHistory>();
        }
        
        [ForeignKey("Sensor")]
        public int Id { get; set; }

        public Sensor Sensor { get; set; }

        [Required]
        public string MeasurementType { get; set; }

        public bool IsConnected { get; set; }

        [Required]
        public bool CurrentValue { get; set; }

        public virtual ICollection<ValueHistory> BoolHistory
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