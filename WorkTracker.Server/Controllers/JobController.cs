using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using WorkTracker.Database.DTOs;
using WorkTracker.Server.Services.Contract;

namespace WorkTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class JobController : Controller
    {
        private IJobService _jobService;
        private IStringLocalizer<Resource> _strLocalizer;

        public JobController(IJobService jobService, IStringLocalizer<Resource> localizerFactory)
        {
            _jobService = jobService;
            _strLocalizer = localizerFactory;
        }

        [HttpGet]
        [Route("GetAllJobsForOwner")]
        public IActionResult GetAllJobsForOwner(int ownerId)
        {
            try
            {
                var result = _jobService.GetAllJobsBelongsToOwner(ownerId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("AddJobForOwner")]
        public IActionResult AddJobForOwner(int ownerId, string jobName)
        {
            try
            {
                var result = _jobService.AddJobForOwner(ownerId, jobName);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
