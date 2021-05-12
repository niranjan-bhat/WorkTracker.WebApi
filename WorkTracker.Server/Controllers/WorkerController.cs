using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using WorkTracker.Database.DTOs;
using WorkTracker.Database.Interfaces;
using WorkTracker.Database.Models;
using WorkTracker.Server.Services;
using WorkTracker.Server.Services.Contract;

namespace WorkTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private IWorkerService _workerService;
        private IStringLocalizer<Resource> _strLocalizer;

        public WorkerController(IWorkerService workerService, IMapper mapper, IConfiguration config, IStringLocalizer<Resource> localizerFactory)
        {
            _workerService = workerService;
            _strLocalizer = localizerFactory;
        }


        [HttpGet]
        [Route("GetAllWorkersForOwner")]
        public IActionResult GetAllWorkersForOwner(int ownerId)
        {
            try
            {
                return Ok(_workerService.GetAllWorkersBelongsToOwner(ownerId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddWorkerForOwner(int ownerId, string workerName, string mobileNumber)
        {
            if (string.IsNullOrEmpty(workerName))
            {
                return BadRequest(_strLocalizer["ErrorInvalidWorkerName"]);
            }

            if (string.IsNullOrEmpty(mobileNumber))
            {
                return BadRequest(_strLocalizer["InvalidWorkerMobileNumber"]);
            }

            try
            {
                _workerService.AddWorkerForOwner(ownerId, workerName, mobileNumber);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}
