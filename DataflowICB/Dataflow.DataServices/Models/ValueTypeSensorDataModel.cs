using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataflow.DataServices.Models
{
    public class ValueTypeSensorDataModel
    {
        public string MeasurementType { get; set; }

        public double MinValue { get; set; }

        public double Maxvalue { get; set; }

        public bool IsInAcceptableRange { get; set; }

        public double CurrentValue { get; set; }

        public ICollection<ValueHistoryDataModel> ValueHistory { get; set; }
    }
}
