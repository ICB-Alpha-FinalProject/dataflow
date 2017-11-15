using DataflowICB.App_Start.Contracts;
using DataflowICB.App_Start.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Dataflow.Services
{

    public class GmailEmailService : IEmailService
    {
        private readonly SmtpConfiguration config;

        private const string GmailUserNameKey = "GmailUserName";
        private const string GmailPasswordKey = "GmailPassword";
        private const string GmailHostKey = "GmailHost";
        private const string GmailPortKey = "GmailPort";
        private const string GmailSslKey = "GmailSsl";

        public GmailEmailService()
        {
            config = new SmtpConfiguration();
            var gmailUserName = ConfigurationManager.AppSettings[GmailUserNameKey];
            var gmailPassword = ConfigurationManager.AppSettings[GmailPasswordKey];
            var gmailHost = ConfigurationManager.AppSettings[GmailHostKey];
            var gmailPort = Int32.Parse(ConfigurationManager.AppSettings[GmailPortKey]);
            var gmailSsl = Boolean.Parse(ConfigurationManager.AppSettings[GmailSslKey]);
            config.Username = gmailUserName;
            config.Password = gmailPassword;
            config.Host = gmailHost;
            config.Port = gmailPort;
            config.Ssl = gmailSsl;
        }

        public bool SendEmailMessage(EmailMessage message)
        {
            var success = false;
            try
            {
                var smtp = new SmtpClient
                {
                    Host = config.Host,
                    Port = config.Port,
                    EnableSsl = config.Ssl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(config.Username, config.Password)
                };

                string from = config.Username;
                string to = message.ToEmail;

                using (var smtpMessage = new MailMessage(from, to))
                {
                    smtpMessage.Subject = message.Subject;
                    smtpMessage.Body = message.Body;
                    smtpMessage.IsBodyHtml = message.IsHtml;
                    smtp.Send(smtpMessage);
                }

                success = true;
            }
            catch (Exception ex)
            {
                //throw;
            }

            return success;
        }
    }
}
