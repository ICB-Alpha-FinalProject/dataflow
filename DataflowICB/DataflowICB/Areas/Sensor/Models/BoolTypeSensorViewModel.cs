﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataflowICB.Areas.Sensor.Models
{
    public class BoolTypeSensorViewModel
    {
        public BoolTypeSensorViewModel()
        {

        }
        

        [Required]
        public bool CurrentValue { get; set; }
    }
}