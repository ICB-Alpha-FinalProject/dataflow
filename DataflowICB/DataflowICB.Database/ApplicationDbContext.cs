using DataflowICB.Database.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public virtual IDbSet<TimeHistory> TimeHistory { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
