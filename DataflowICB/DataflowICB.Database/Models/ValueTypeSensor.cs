using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataflowICB.Database.Models
{
    public class ValueTypeSensor
    {
        private ICollection<ValueHistory> valueHistory;
        public ValueTypeSensor()
        {
            this.valueHistory = new List<ValueHistory>();
        }
        
        [ForeignKey("Sensor")]
        public int Id { get; set; }

        public Sensor Sensor { get; set; }

        [Required]
        public string MeasurementType { get; set; }

        [Required]
        public double MinValue { get; set; }

        [Required]
        public double Maxvalue { get; set; }

        public bool IsInAcceptableRange { get; set; }

        public bool IsConnected { get; set; }

        [Required]
        public double CurrentValue { get; set; }

        public virtual ICollection<ValueHistory> ValueHistory
        {
            get
            {
                return this.valueHistory;
            }
            set
            {
                this.valueHistory = value;
            }
        }
    }
}