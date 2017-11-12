using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataflowICB.Areas.Sensor.Models
{
    public class ValueTypeSensorViewModel
    {
        public ValueTypeSensorViewModel()
        {

        }

        [Required]
        public double MinValue { get; set; }

        [Required]
        public double Maxvalue { get; set; }

        public double LowestValue { get; set; }

        public double HighestValue { get; set; }

        public bool IsInAcceptableRange { get; set; }

    }
}