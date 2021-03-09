using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using WorkTracker.Server.Services.Contract;

namespace WorkTracker.Server.Services
{
    public class EmailService : IEmailService
    {
        private IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(string toAddress, string subject, string htmlbody)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["SmtpServer:UserName"]));
            email.To.Add(MailboxAddress.Parse(toAddress));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = htmlbody };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_config["SmtpServer:Address"], int.Parse(_config["SmtpServer:Port"]), SecureSocketOptions.None);
            smtp.Authenticate(_config["SmtpServer:UserName"], _config["SmtpServer:Password"]); 
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public string GenerateHtmlBodyForEmailVerification(int otp)
        {
            return "<h2>Greetings from WorkTracker</h2><p>Your verification code is:</p> " + otp;
        }
    }
}
