using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataflowICB.App_Start.Models
{
    public class EmailMessage
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
    }
}