using Dataflow.DataServices.Contracts;
using DataflowICB.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Dataflow.DataServices.Models
{
    public class UserDataModel : IUserDataModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public ICollection<SensorDataModel> MySensors { get; set; }

        public ICollection<SensorDataModel> SharedSensors { get; set; }

        public bool IsDeleted { get; set; }

        public static Expression<Func<ApplicationUser, UserDataModel>> Create
        {
            get
            {
                return u => new UserDataModel()
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email,
                    IsDeleted = u.IsDeleted
                };
            }
        }

        public static UserDataModel Convert (ApplicationUser user)
        {
            return new UserDataModel()
                   {
                       Id = user.Id,
                       Username = user.UserName,
                       Email = user.Email,
                       IsDeleted = user.IsDeleted
                   };
        }
    }
}
