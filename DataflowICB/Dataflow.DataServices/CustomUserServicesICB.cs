using Bytes2you.Validation;
using Dataflow.DataServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataflowICB.Database.Models;
using DataflowICB.Database;
using DataflowICB;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Dataflow.DataServices.Models;

namespace Dataflow.DataServices
{
    public class CustomUserServicesICB : IUserServices
    {
        private readonly ApplicationDbContext dbContext;

        public CustomUserServicesICB(ApplicationDbContext dbContext)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.dbContext = dbContext;
        }

        public void EditUser(IUserDataModel editedUser)
        {
            Guard.WhenArgument(editedUser, "editedUser").IsNull().Throw();

            var user = this.dbContext.Users.First(u => u.Id == editedUser.Id);
            if (user != null)
            {
                user.UserName = editedUser.Username;
                user.Email = editedUser.Email;
                user.IsDeleted = editedUser.IsDeleted;

                this.dbContext.SaveChanges();
            }
        }

        public UserDataModel GetUser(string username)
        {
            Guard.WhenArgument(username, "username").IsNull().Throw();

            var user = this.dbContext.Users.First(u => u.UserName == username);
            if (user == null)
            {
                throw new ArgumentException($"No user with username {username}!");
            }

            return UserDataModel.Convert(user);
        }

        public ICollection<UserDataModel> GetAllUsers()
        {
            return this.dbContext.Users.Select(UserDataModel.Create).ToList();
        }
    }
}
