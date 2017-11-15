using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataflowICB.App_Start.Models
{
    public class SmtpConfiguration
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
    }
}