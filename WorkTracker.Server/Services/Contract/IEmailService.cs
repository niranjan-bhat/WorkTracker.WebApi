using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkTracker.Server.Services.Contract
{
    public interface IEmailService
    {
        void SendEmail(string toAddress, string subject, string htmlbody);
        string GenerateHtmlBodyForEmailVerification(int otp);
    }
}
