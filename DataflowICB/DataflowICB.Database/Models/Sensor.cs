using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string URL { get; set; }

        [Required]
        [Range(1, maxPollingInterval)]
        public uint PollingInterval { get; set; }

        public virtual ValueTypeSensor ValueTypeSensor { get; set; }

        public string ValueTypeSensorId { get; set; }

        public virtual BoolTypeSensor BoolTypeSensor { get; set; }

        public string BoolTypeSensorId { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public SensorRangeValidity SensorValidity { get; set; }        
        
        [Required]
        public virtual ApplicationUser Creator { get; set; }

        [Required]
        public string CreatorId { get; set; }

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
    }
}
