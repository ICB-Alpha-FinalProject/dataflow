using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataflowICB.Database.Models
{
    public class ValueTypeSensor
    {
        private ICollection<TimeHistory> valueHistory;

        public ValueTypeSensor()
        {
            this.valueHistory = new List<TimeHistory>();
        }

        [Key, ForeignKey("SensorModel")]
        public string Id { get; set; }

        [Required]
        public string MeasurementType { get; set; }

        public string SensorModelId { get; set; }

        public virtual Sensor SensorModel { get; set; }

        [Required]
        public double MinValue { get; set; }

        [Required]
        public double Maxvalue { get; set; }

        public bool IsInAcceptableRange { get; set; }

        public bool IsConnected { get; set; }

        [Required]
        public double CurrentValue { get; set; }

        public virtual ICollection<TimeHistory> ValueHistory
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