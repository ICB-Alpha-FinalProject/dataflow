using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataflowICB.Areas.Sensor.Models
{
    public class SensorViewModel
    {
        public SensorViewModel()
        {

        }
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 3)]
        public string Description { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int PollingInterval { get; set; }
        [Required]
        public string MeasurementType { get; set; }
        [Required]
        public bool IsPublic { get; set; }

    }
}