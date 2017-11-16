using Dataflow.DataServices.Models;
using DataflowICB.App_Start;
using DataflowICB.Database.Models;
using SensorApiModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace DataflowICB.Areas.Sensor.Models
{
    public class SensorViewModel
    {
        public SensorViewModel()
        {

        }
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 20 characters")]
        public string Name { get; set; }

        [StringLength(250, MinimumLength = 3)]
        public string Description { get; set; }

        public string Url { get; set; }
        
        public string CurrentValue { get; set; }
        
        [Remote("CheckLowerRange","Sensor","Sensor", AdditionalFields = "MaxValue,LowestValue,HighestValue",
            ErrorMessage = "Minimum must be between LOWEST and HIGHEST value!")]
        public double MinValue { get; set; }

        [Remote("CheckUpperRange", "Sensor", "Sensor", AdditionalFields = "MinValue, LowestValue,HighestValue",
            ErrorMessage = "Maximum must be between LOWEST and HIGHEST value!")]
        public double MaxValue { get; set; }


        public double? LowestValue { get; set; }

        public double? HighestValue { get; set; }

        public string MeasurementType { get; set; }

        [Required]
        [Remote("CheckPollingInterval", "Sensor","Sensor",AdditionalFields = "MinPollingInterval",
            ErrorMessage = "Polling interval must be greater than Min Polling Interval")]
        public int PollingInterval { get; set; }

        public int MinPollingInterval { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public bool IsShared { get; set; }

        public bool IsValueType { get; set; }

        public bool IsConnected { get; set; }

        public string CreatorUsername { get; set; }

        public string ShareWithUser { get; set; }

        public ICollection<string> SharedWithUsers { get; set; }

        public bool IsReadOnly { get; set; }

    }
}
