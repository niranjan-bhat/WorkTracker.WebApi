using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Localization;
using WorkTracker.Database.DTOs;
using WorkTracker.Database.Interfaces;
using WorkTracker.Database.Models;
using WorkTracker.Server.Services.Contract;

namespace WorkTracker.Server.Services
{
    public class AssignmentService : IAssignmentService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IHelper _helper;
        private IStringLocalizer _strLocalizer;

        public AssignmentService(IUnitOfWork unitOfWork, IMapper mapper, IHelper helper, IStringLocalizer<Resource> stringLocalizer)
        {
            _mapper = mapper;
            _helper = helper;
            _unitOfWork = unitOfWork;
            _strLocalizer = stringLocalizer;
        }

        /// <summary>
        /// Returns all the assignment within the given date range corresponding to specific worker and owner.
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="workerId"></param>
        /// <returns></returns>
        public List<AssignmentDTO> GetAssignments(int ownerId, DateTime startDateTime, DateTime endDateTime, int workerId)
        {
            if (startDateTime == null || endDateTime == null || startDateTime > endDateTime)
                throw new Exception(_strLocalizer["ErrorInvalidDate"]);

            var worker = _unitOfWork.Workers.GetByID(workerId);
            if (worker == null)
            {
                throw new Exception(_strLocalizer["ErrorWorkerNotFound"]);
            }

            var assignmentList = _unitOfWork.Assignments.Get(x => x.WorkerId == workerId &&
                                                                  x.AssignedDate >= startDateTime &&
                                                                  x.AssignedDate <= endDateTime,null,$"Jobs,Comments")?.
                                                                  Select(x => _mapper.Map<AssignmentDTO>(x)).ToList();

            return assignmentList;
        }

        /// <summary>
        /// Updates the assignment
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="assignment"></param>
        /// <returns></returns>
        public AssignmentDTO UpdateAssignment(int ownerId, AssignmentDTO assignment)
        {
            var worker = GetWorkersFromDb(assignment.WorkerId, ownerId);

            var jobs = GetJObsFromDb(assignment.Jobs?.ToList(), ownerId);

            var insertedEntity = _unitOfWork.Assignments.Update(new Assignment()
            {
                Worker = worker,
                Jobs = jobs,
                Wage = assignment.Wage,
                AssignedDate = assignment.AssignedDate,
            });
            _unitOfWork.Commit();

            return _mapper.Map<AssignmentDTO>(insertedEntity);
        }

        /// <summary>
        /// Adds single comment to assignment
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public CommentDTO AddComment(int assignmentId, string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
                throw new Exception(_strLocalizer["ErrorComment"]);

            var assignment = _unitOfWork.Assignments.GetByID(assignmentId);

            if (assignment == null)
            {
                throw new Exception(_strLocalizer["ErrorAssignmentNotFound"]);
            }

            var insertedComment = _unitOfWork.Comments.Insert(new Comment()
            {
                AddeTime = DateTime.Now,
                Assignment = assignment,
                OwnerComment = comment
            });
            _unitOfWork.Commit();

            return _mapper.Map<CommentDTO>(insertedComment);
        }

        /// <summary>
        /// Insert assignment to the database
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="wage"></param>
        /// <param name="workerId"></param>
        /// <param name="assignedDate"></param>
        /// <param name="jobs"></param>
        /// <returns></returns>
        public AssignmentDTO AddAssignment(int ownerId, int wage, int workerId, DateTime assignedDate, List<JobDTO> jobs)
        {
            var worker = GetWorkersFromDb(workerId, ownerId);

            var dbJobs = GetJObsFromDb(jobs, ownerId);

            var insertedEntity = _unitOfWork.Assignments.Insert(new Assignment()
            {
                Worker = worker,
                Jobs = dbJobs,
                Wage = wage,
                AssignedDate = assignedDate,
            });
            _unitOfWork.Commit();

            return _mapper.Map<AssignmentDTO>(insertedEntity);
        }

        /// <summary>
        /// Retrieves the worker from the database
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        private Worker GetWorkersFromDb(int workerId, int ownerId)
        {
            var worker = _unitOfWork.Workers.GetByID(workerId);
            if (worker == null || worker.OwnerId != ownerId)
            {
                throw new Exception(_strLocalizer["ErrorWorkerNotFound"]);
            }
            return worker;
        }

        /// <summary>
        /// Retrieves the the job from the database corresponding to owner
        /// </summary>
        /// <param name="jobs"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        private List<Job> GetJObsFromDb(List<JobDTO> jobs, int ownerId)
        {
            if (jobs == null)
            {
                throw new Exception(_strLocalizer["ErrorInvalidJob"]);
            }
            var dbjobs = new List<Job>();

            foreach (var job in jobs)
            {
                var dbjob = _unitOfWork.Jobs.GetByID(job.Id);
                if (dbjob == null || dbjob.OwnerId != ownerId)
                {
                    throw new Exception(_strLocalizer["ErrorInvalidJob"]);
                }
                dbjobs.Add(dbjob);
            }

            return dbjobs;
        }
    }
}
