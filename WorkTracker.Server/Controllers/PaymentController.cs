using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using WorkTracker.Database.DTOs;
using WorkTracker.Server.Services.Contract;

namespace WorkTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PaymentController : Controller
    {
        private IPaymentService _paymentService;
        private IStringLocalizer<Resource> _strLocalizer;

        public PaymentController(IPaymentService paymentService, IMapper mapper, IStringLocalizer<Resource> localizerFactory)
        {
            _paymentService = paymentService;
            _strLocalizer = localizerFactory;
        }



        [HttpGet]
        public IActionResult GetAll(int workerId)
        {
            try
            {
                return Ok(_paymentService.GetAllPayments(workerId));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public IActionResult Post(PaymentDTO paymentDto)
        {
            try
            {
                return Ok(_paymentService.AddPayment(paymentDto));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
