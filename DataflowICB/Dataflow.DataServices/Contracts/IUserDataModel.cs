using Dataflow.DataServices.Models;
using DataflowICB.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dataflow.DataServices.Contracts
{
    public interface IUserDataModel
    {
        string Id { get; set; }

        string Username { get; set; }

        string Email { get; set; }

        ICollection<SensorDataModel> MySensors { get; set; }

        ICollection<SensorDataModel> SharedSensors { get; set; }

        bool IsDeleted { get; set; }
    }
}
