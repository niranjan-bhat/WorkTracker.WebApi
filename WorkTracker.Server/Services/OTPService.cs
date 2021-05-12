using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTracker.Server.Services.Contract;

namespace WorkTracker.Server.Services
{
    public class OTPService : IOTPService
    {
        public int GenerateOTP()
        {
            int min = 1000;
            int max = 9999;
            var otp = 5555;

            Random rdm = new Random();
            otp = rdm.Next(min, max);
            return otp;
        }
    }
}
