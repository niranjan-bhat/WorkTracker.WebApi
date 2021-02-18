using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using WorkTracker.Database.DTOs;
using WorkTracker.Server.Services.Contract;

namespace WorkTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AssignmentController : Controller
    {
        private IAssignmentService _assignmentService;
        private IStringLocalizer<Resource> _strLocalizer;

        public AssignmentController(IAssignmentService assignmentService, IStringLocalizer<Resource> localizerFactory)
        {
            _assignmentService = assignmentService;
            _strLocalizer = localizerFactory;
        }

        [SwaggerOperation(Summary = "Add a assignment to a worker")]
        [HttpPost]
        public IActionResult AddAssignment(int ownerId, int wage, int workerId, DateTime assignedDate, [FromBody] List<JobDTO> jobs)
        {
            try
            {
                var result = _assignmentService.AddAssignment(ownerId, wage, workerId, assignedDate, jobs);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message);
            }
        }

        [SwaggerOperation(Summary = "Add a comment to a assignment")]
        [HttpPost]
        [Route("AddComment")]
        public IActionResult AddComment(int assignmentId, string comment)
        {
            try
            {
                var result = _assignmentService.AddComment(assignmentId, comment);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message);
            }
        }

        [SwaggerOperation(Summary = "Retrieve all the assignment belongs to given range of dates and belongs to this worker ")]
        [HttpGet]
        [Route("GetAssignment")]
        public IActionResult GetAssignment(int ownerId, DateTime startDateTime, DateTime endDateTime, int workerId)
        {
            try
            {
                var result = _assignmentService.GetAssignments(ownerId, startDateTime, endDateTime, workerId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message);
            }
        }
    }
}
