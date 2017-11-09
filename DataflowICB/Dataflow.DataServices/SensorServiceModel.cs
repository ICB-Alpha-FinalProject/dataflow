using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataflow.DataServices
{
    public class SensorServiceModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CurrentValue { get; set; }

        public bool IsPublic { get; set; }

        public bool IsShared { get; set; }


    }
}
