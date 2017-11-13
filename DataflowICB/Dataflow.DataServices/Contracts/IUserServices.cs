using Dataflow.DataServices.Models;
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
        UserDataModel GetUser(string username);

        ICollection<UserDataModel> GetAllUsers();

        void EditUser(IUserDataModel editedUser);
    }
}
