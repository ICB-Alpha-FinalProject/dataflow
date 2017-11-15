using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorApiModels.SensorViewModels
{
    public class RealTimeSensor
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public bool IsValueType { get; set; }

    }
}
