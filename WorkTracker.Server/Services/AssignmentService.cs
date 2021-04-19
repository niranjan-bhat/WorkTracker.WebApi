using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Localization;
using Org.BouncyCastle.Crypto;
using WorkTracker.Database.DTO;
using WorkTracker.Database.DTOs;
using WorkTracker.Database.Interfaces;
using WorkTracker.Database.Models;
using WorkTracker.Server.Services.Contract;

namespace WorkTracker.Server.Services
{
    public class AssignmentService : IAssignmentService
    {
        private IHelper _helper;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _strLocalizer;
        private readonly IUnitOfWork _unitOfWork;

        public AssignmentService(IUnitOfWork unitOfWork, IMapper mapper, IHelper helper,
            IStringLocalizer<Resource> stringLocalizer)
        {
            _mapper = mapper;
            _helper = helper;
            _unitOfWork = unitOfWork;
            _strLocalizer = stringLocalizer;
        }

        /// <summary>
        ///     Returns all the assignment within the given date range corresponding to specific worker and owner.
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="workerId"></param>
        /// <returns></returns>
        public List<AssignmentDTO> GetAllAssignments(int ownerId, DateTime startDateTime, DateTime endDateTime, int? workerId)
        {
            if (startDateTime == null || endDateTime == null || startDateTime > endDateTime)
                throw new Exception(_strLocalizer["ErrorInvalidDate"]);

            if ((endDateTime - startDateTime).TotalDays > 31)
            {
                throw new Exception(_strLocalizer["ErrorOverflowDateRange"]);
            }

            if (workerId.HasValue)
            {
                var worker = _unitOfWork.Workers.GetByID(workerId);
                if (worker == null) throw new Exception(_strLocalizer["ErrorWorkerNotFound"]);
            }

            var assignmentList = _unitOfWork.Assignments.Get(x => (workerId == null || x.WorkerId == workerId.Value) &&
                                                                  x.AssignedDate >= startDateTime &&
                                                                  x.AssignedDate <= endDateTime, null,
                                                                  $"{nameof(Assignment.Jobs)},{nameof(Assignment.Comments)}")
                ?.Select(x => _mapper.Map<AssignmentDTO>(x)).ToList();

            return assignmentList;
        }

        /// <summary>
        ///  Delete the assignments for a given date
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="assignedDate"></param>
        /// <returns></returns>
        public bool DeleteAssignments(int ownerId, DateTime assignedDate)
        {
            var assignmentsToDelete = _unitOfWork.Assignments.Get(x => x.OwnerId == ownerId &&
                                                                       x.AssignedDate == assignedDate,null,
                                        $"{nameof(Assignment.Jobs)},{nameof(Assignment.Comments)}")?.ToList();

            if (assignmentsToDelete != null)
            {
                foreach (var assignment in assignmentsToDelete)
                {
                    _unitOfWork.Assignments.Delete(assignment);
                }
                _unitOfWork.Commit();
            }

            return true;
        }

        private bool CheckJobsAreSame(List<JobDTO> jobsToUpdate, List<JobDTO> dbJobs)
        {
            jobsToUpdate = jobsToUpdate == null ? new List<JobDTO>() : jobsToUpdate;
            dbJobs = dbJobs == null ? new List<JobDTO>() : dbJobs;

            var dbIds = dbJobs?.Select(x => x.Id).OrderByDescending(x => x);
            var jobsToUpdateIds = jobsToUpdate?.Select(x => x.Id).OrderByDescending(x => x);


            return dbIds.All(jobsToUpdateIds.Contains);
        }

        /// <summary>
        ///     Adds single comment to assignment
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public CommentDTO AddComment(int assignmentId, string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
                throw new Exception(_strLocalizer["ErrorComment"]);

            var assignment = _unitOfWork.Assignments.GetByID(assignmentId);

            if (assignment == null) throw new Exception(_strLocalizer["ErrorAssignmentNotFound"]);

            var insertedComment = _unitOfWork.Comments.Insert(new Comment
            {
                AddeTime = DateTime.Now,
                Assignment = assignment,
                OwnerComment = comment
            });
            _unitOfWork.Commit();

            return _mapper.Map<CommentDTO>(insertedComment);
        }

        public AssignmentDTO GetAssignmentById(int assignmentId)
        {
            var result = _unitOfWork.Assignments.Get(x => x.Id == assignmentId, null,
                    $"{nameof(Assignment.Jobs)},{nameof(Assignment.Comments)},{nameof(Assignment.Worker)}")
                ?.FirstOrDefault();
            if (result == null) throw new Exception(_strLocalizer["ErrorAssignmentNotFound"]);
            return _mapper.Map<AssignmentDTO>(result);
        }

        /// <summary>
        ///     Insert assignment to the database
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="wage"></param>
        /// <param name="workerId"></param>
        /// <param name="assignedDate"></param>
        /// <param name="jobs"></param>
        /// <returns></returns>
        public AssignmentDTO AddAssignment(int ownerId, int wage, int workerId, DateTime assignedDate,
            List<JobDTO> jobs)
        {
            var worker = GetWorkersFromDb(workerId, ownerId);

            var dbJobs = GetJobsFromDb(jobs, ownerId);

            var owner = GetOwnerByID(ownerId);

            var insertedEntity = _unitOfWork.Assignments.Insert(new Assignment
            {
                Worker = worker,
                Jobs = dbJobs,
                Wage = wage,
                Owner = owner,
                AssignedDate = assignedDate
            });
            _unitOfWork.Commit();

            var entityInserted = _unitOfWork.Assignments.GetByID(insertedEntity.Id);

            return _mapper.Map<AssignmentDTO>(entityInserted);
        }

        //public AssignmentDTO GetAssignmentForDate(int workerId, DateTime date, int ownerId)
        //{
        //    var worker = _unitOfWork.Workers.GetByID(workerId);
        //    if (worker == null || worker.OwnerId != ownerId) throw new Exception(_strLocalizer["ErrorWorkerNotFound"]);

        //    var assignment = _unitOfWork.Assignments.Get(x => x.AssignedDate == date &&
        //                                                      x.WorkerId == workerId, null,
        //        $"{nameof(Assignment.Jobs)},{nameof(Assignment.Comments)}").FirstOrDefault();

        //    if (assignment == null) throw new Exception(_strLocalizer["ErrorAssignmentNotFound"]);

        //    return _mapper.Map<AssignmentDTO>(assignment);
        //}

        /// <summary>
        ///     Retrieves the worker from the database
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        private Worker GetWorkersFromDb(int workerId, int ownerId)
        {
            var worker = _unitOfWork.Workers.GetByID(workerId);
            if (worker == null || worker.OwnerId != ownerId) throw new Exception(_strLocalizer["ErrorWorkerNotFound"]);
            return worker;
        }

        /// <summary>
        ///     Retrieves the the job from the database corresponding to owner
        /// </summary>
        /// <param name="jobs"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        private List<Job> GetJobsFromDb(List<JobDTO> jobs, int ownerId)
        {
            if (jobs == null) throw new Exception(_strLocalizer["ErrorInvalidJob"]);
            var dbjobs = new List<Job>();

            foreach (var job in jobs)
            {
                var dbjob = _unitOfWork.Jobs.GetByID(job.Id);
                if (dbjob == null || dbjob.OwnerId != ownerId) throw new Exception(_strLocalizer["ErrorInvalidJob"]);
                dbjobs.Add(dbjob);
            }

            return dbjobs;
        }

        private Owner GetOwnerByID(int ownerId)
        {
            var owner = _unitOfWork.Owners.GetByID(ownerId);
            if (owner == null)
                throw new Exception(_strLocalizer["OwnerNotFound"]);

            return owner;
        }
    }
}