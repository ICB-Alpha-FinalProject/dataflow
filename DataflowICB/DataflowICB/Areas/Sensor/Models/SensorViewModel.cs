﻿using DataflowICB.App_Start;
using DataflowICB.Database.Models;
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
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 20 characters")]
        public string Name { get; set; }

        [StringLength(250, MinimumLength = 3)]
        public string Description { get; set; }
        
        public string Url { get; set; }

        public ValueTypeSensorViewModel ValueTypeSensor { get; set; }

        public BoolTypeSensorViewModel BoolTypeSensor { get; set; }
        
        public string MeasurementType { get; set; }

        [Required]
        [Range(1, Constants.MaxPollingInterval)]
        public int PollingInterval { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public bool IsValueType { get; set; }
        
        public string CreatorUsername { get; set; }

        public ICollection<string> SharedWithUsers { get; set; }
    }
}