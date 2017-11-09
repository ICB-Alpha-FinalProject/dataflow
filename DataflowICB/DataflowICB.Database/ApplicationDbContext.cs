using DataflowICB.Database.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataflowICB.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("SensorSystem", throwIfV1Schema: false)
        {

        }
        public virtual IDbSet<Sensor> Sensors { get; set; }
        public virtual IDbSet<BoolTypeSensor> BoolSensors { get; set; }
        public virtual IDbSet<ValueTypeSensor> ValueSensors { get; set; }
        public virtual IDbSet<ValueHistory> ValueHistory { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.SharedSensors)
                .WithMany(s => s.SharedWithUsers)
                .Map(us =>
                {
                    us.MapLeftKey("UserRefId");
                    us.MapRightKey("SensorRefId");
                    us.ToTable("UserSharedSensors");
                });


        }

    }
}
