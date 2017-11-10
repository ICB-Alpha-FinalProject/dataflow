using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataflowICB.Models.DataApi
{
    public class SensorApiUpdate
    {
        [JsonProperty("timeStamp")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime TimeStamp { get; set; }

        public string Value { get; set; }
    }
}