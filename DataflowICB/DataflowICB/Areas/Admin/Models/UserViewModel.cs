using Dataflow.DataServices.Models;
using DataflowICB.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DataflowICB.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsDeleted { get; set; }

        public static Expression<Func<ApplicationUser, UserViewModel>> Create
        {
            get
            {
                return u => new UserViewModel()
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email,
                    IsDeleted = u.IsDeleted
                };
            }
        }

        public static ICollection<UserViewModel> Convert(IEnumerable<UserDataModel> applicationUsers)
        {
            ICollection<UserViewModel> usersViewModel = new List<UserViewModel>();
            foreach (var appUser in applicationUsers)
            {
                usersViewModel.Add(new UserViewModel
                {
                    Email = appUser.Email,
                    Username = appUser.Username,
                    Id = appUser.Id,
                    IsDeleted = appUser.IsDeleted
                });
            }

            return usersViewModel;
        }
    }
}