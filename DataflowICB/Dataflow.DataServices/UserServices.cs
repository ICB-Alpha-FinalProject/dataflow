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
            this.dbContext = dbContext;
        }

        public void EditUser(string usernameOld, string username, string email)
        {
            var user = GetUser(usernameOld);

            user.UserName = username;
            user.Email = email;

            this.dbContext.SaveChanges();
        }

        public ApplicationUser GetUser(string username)
        {
            var user = this.dbContext.Users.First(u => u.UserName == username);
            if (user == null)
            {
                throw new ArgumentException($"No user with username {username}!");
            }

            return user;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return this.dbContext
                .Users
                .Select(u => new ApplicationUser()
                {
                    Email = u.Email,
                    UserName = u.UserName,
                })
                .ToList();
        }
    }
}
