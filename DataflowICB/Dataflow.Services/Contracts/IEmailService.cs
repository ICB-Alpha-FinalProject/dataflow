using DataflowICB.App_Start.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataflowICB.App_Start.Contracts
{
    public interface IEmailService
    {
        bool SendEmailMessage(EmailMessage message);
    }
}