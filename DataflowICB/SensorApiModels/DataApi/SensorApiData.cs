using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataflowICB.Models.DataApi
{
    public class SensorApiData
    {
        [JsonProperty("sensorId")]
        public string Id { get; set; }

        public string Tag { get; set; }

        public string Description { get; set; }

        [JsonProperty("minPollingIntervalInSeconds")]
        public int MinPollingIntervalInSeconds { get; set; }

        [JsonProperty("measureType")]
        public string MeasureType { get; set; }

        public bool IsValueType { get; set; }

        public double? LowestValue { get; set; }

        public double? Highestvalue { get; set; }

    }
}