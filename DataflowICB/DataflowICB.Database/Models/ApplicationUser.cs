using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataflowICB.Database.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        private ICollection<Sensor> mySensors;
        private ICollection<Sensor> sharedSensors;

        public ApplicationUser()
        {
            this.mySensors = new HashSet<Sensor>();
            this.sharedSensors = new HashSet<Sensor>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<Sensor> MySensors
        {
            get
            {
                return this.mySensors;
            }
            set
            {
                mySensors = value;
            }
        }

        public virtual ICollection<Sensor> SharedSensors
        {
            get
            {
                return this.sharedSensors;
            }
            set
            {
                sharedSensors = value;
            }
        }

        public bool IsDeleted { get; set; }
    }
}
