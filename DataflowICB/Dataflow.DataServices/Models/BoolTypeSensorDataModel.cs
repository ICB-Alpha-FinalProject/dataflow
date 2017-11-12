using System.Collections.Generic;

namespace Dataflow.DataServices.Models
{
    public class BoolTypeSensorDataModel
    {
        public string MeasurementType { get; set; }

        public bool CurrentValue { get; set; }

        public ICollection<ValueHistoryDataModel> BoolHistory { get; set; }
    }
}
