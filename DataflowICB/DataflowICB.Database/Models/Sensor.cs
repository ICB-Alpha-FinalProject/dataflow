using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataflowICB.Database.Models
{
    public class Sensor
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string URL { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public uint PollingInterval { get; set; }

        [Required]
        public string MeasurementType { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public SensorRangeValidity SensorValidity { get; set; }

        //Time History

        [Required]
        public string SensorValue { get; set; }


        public DateTime LastUpdate { get; set; }
    }
}
