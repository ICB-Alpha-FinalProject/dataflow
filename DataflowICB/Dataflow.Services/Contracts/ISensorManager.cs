using DataflowICB.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataflow.Services.Contracts
{
    public interface ISensorManager
    {
        Task UpdateSensors(IEnumerable<Sensor> sensors);
    }
}
