using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataflowICB.Areas.Admin.Models
{
    public class AdminSensorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string URL { get; set; }

        public int PollingInterval { get; set; }

        public bool IsBoolType { get; set; }

        public bool IsPublic { get; set; }

        public string OwnerId { get; set; }

        public string Owner { get; set; }

        public ICollection<string> SharedWithUsers { get; set; }

        public bool IsDeleted { get; set; }
    }
}