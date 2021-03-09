using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WorkTracker.Server.Services.Contract;

namespace WorkTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiscellaneousController : ControllerBase
    {
        private IOTPService _otpService;
        private IEmailService _emailService;
        public MiscellaneousController(IOTPService otpService, IEmailService emailService)
        {
            _otpService = otpService;
            _emailService = emailService;
        }

        [HttpGet]
        [Route("GetOtpForUser")]
        public IActionResult GetOtpForUser(string email)
        {
            var otp = _otpService.GenerateOTP();
            if (email != null)
            {
                _emailService.SendEmail(email, @"One Time Password(OTP) from WorkTracker", _emailService.GenerateHtmlBodyForEmailVerification(otp));
            }
            return Ok(otp);
        }
    }
}
