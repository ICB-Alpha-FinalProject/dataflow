using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataflowICB.Database.Models
{
    public class Sensor
    {
        private const int maxPollingInterval = 86400;
        private ICollection<ApplicationUser> sharedWithUsers;

        public Sensor()
        {
            this.sharedWithUsers = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string URL { get; set; }

        [Required]
        [Range(1, maxPollingInterval)]
        public int PollingInterval { get; set; }

        public virtual ValueTypeSensor ValueTypeSensor { get; set; }

        public virtual BoolTypeSensor BoolTypeSensor { get; set; }

        public bool IsBoolType { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public bool IsShared { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<ApplicationUser> SharedWithUsers
        {
            get
            {
                return this.sharedWithUsers;
            }
            set
            {
                this.sharedWithUsers = value;
            }
        }

        public double SensorCoordinatesX { get; set; }

        public double SensorCoordinatesY { get; set; }

        public DateTime LastUpdate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
