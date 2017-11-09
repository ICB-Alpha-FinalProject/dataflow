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

namespace Dataflow.DataServices
{
    public class UserServices : IUserServices
    {
        private readonly ApplicationDbContext dbContext;

        public UserServices(ApplicationDbContext dbContext)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.dbContext = dbContext;
        }

        public void EditUser(ApplicationUser editedUser)
        {
            Guard.WhenArgument(editedUser, "editedUser").IsNull().Throw();

            var user = this.dbContext.Users.First(u => u.Id == editedUser.Id);
            if (user != null)
            {
                user.UserName = editedUser.UserName;
                user.Email = editedUser.Email;
                user.IsDeleted = editedUser.IsDeleted;

                this.dbContext.SaveChanges();
            }
        }

        public ApplicationUser GetUser(string username)
        {
            Guard.WhenArgument(username, "username").IsNull().Throw();

            var user = this.dbContext.Users.First(u => u.UserName == username);
            if (user == null)
            {
                throw new ArgumentException($"No user with username {username}!");
            }

            return user;
        }

        public ICollection<ApplicationUser> GetAllUsers()
        {
            return this.dbContext.Users.ToList();
        }
    }
}
