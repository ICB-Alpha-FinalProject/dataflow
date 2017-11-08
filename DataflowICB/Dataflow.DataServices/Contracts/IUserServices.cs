using DataflowICB.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataflow.DataServices.Contracts
{
    public interface IUserServices
    {
        ApplicationUser GetUser(string username);

        IEnumerable<ApplicationUser> GetAllUsers();
    }
}
