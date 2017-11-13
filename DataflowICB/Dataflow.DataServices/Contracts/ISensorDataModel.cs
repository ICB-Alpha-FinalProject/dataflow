using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataflow.DataServices.Contracts
{
    public interface ISensorDataModel
    {
        int Id { get; set; }

        string Name { get; set; }

        string MeasurementType { get; set; }

        string Description { get; set; }

        string URL { get; set; }

        int PollingInterval { get; set; }

        string CurrentValue { get; set; }

        double MaxValue { get; set; }

        double MinValue { get; set; }

        bool IsBoolType { get; set; }

        bool IsPublic { get; set; }

        bool IsShared { get; set; }

        bool IsConnected { get; set; }

        string OwnerId { get; set; }

        string Owner { get; set; }

        ICollection<string> SharedWithUsers { get; set; }

        double SensorCoordinatesX { get; set; }

        double SensorCoordinatesY { get; set; }

        DateTime LastUpdate { get; set; }

        bool IsDeleted { get; set; }
    }
}
