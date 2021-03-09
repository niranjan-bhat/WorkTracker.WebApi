using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkTracker.Server.Services.Contract
{
    public interface IMailService
    {
        void Send(string from, string to, string subject, string html);
    }
}
