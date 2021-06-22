using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Localization;
using Org.BouncyCastle.Crypto;
using WorkTracker.Database.DTO;
using WorkTracker.Database.DTOs;
using WorkTracker.Database.Interfaces;
using WorkTracker.Database.Models;
using WorkTracker.Server.Exceptions;
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
        public List<AssignmentDTO> GetAllAssignments(int ownerId, DateTime startDateTime, DateTime endDateTime, int? workerId, int? jobId)
        {
            if (startDateTime == null || endDateTime == null || startDateTime > endDateTime)
                throw new Exception(_strLocalizer["ErrorInvalidDate"]);

            if ((endDateTime - startDateTime).TotalDays > 5000)
            {
                throw new Exception(_strLocalizer["ErrorOverflowDateRange5000Days"]);
            }

            var assignmentList = _unitOfWork.Assignments.Get(x =>x.OwnerId == ownerId &&
                                                                  x.AssignedDate >= startDateTime &&
                                                                  x.AssignedDate <= endDateTime, null,
                $"{nameof(Assignment.Jobs)},{nameof(Assignment.Comments)}");

            if (workerId.HasValue)
            {
                var worker = _unitOfWork.Workers.GetByID(workerId);
                if (worker == null) throw new Exception(_strLocalizer["ErrorWorkerNotFound"]);

                assignmentList = assignmentList.Where(x => x.WorkerId == workerId);
            }

            if (jobId.HasValue)
            {
                var job = GetJobsFromDb(new List<JobDTO>()
                {
                    new JobDTO() {Id = jobId.Value}
                }, ownerId).FirstOrDefault();

                assignmentList = assignmentList.Where(x => x.Jobs.Contains(job));
            }


            var result = assignmentList?.Select(x => _mapper.Map<AssignmentDTO>(x)).ToList();

            return result;
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
                                                                       x.AssignedDate == assignedDate, null,
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

            if (assignment == null)
                throw new WtException(_strLocalizer["ErrorAssignmentNotFound"], Constants.ASSIGNMENT_NOT_FOUND);

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
            if (result == null) throw new WtException(_strLocalizer["ErrorAssignmentNotFound"], Constants.ASSIGNMENT_NOT_FOUND);
            return _mapper.Map<AssignmentDTO>(result);
        }

        /// <summary>
        /// Add assignments in bulk
        /// </summary>
        /// <param name="assignments"></param>
        /// <returns></returns>
        public List<AssignmentDTO> AddAssignments(List<AssignmentDTO> assignments)
        {
            if (assignments == null || assignments.Count == 0)
            {
                throw new InvalidDataException();
            }

            var insertedRecords = new List<AssignmentDTO>();

            int ownerId = assignments.FirstOrDefault().OwnerId;

            foreach (var assignment in assignments)
            {
                var dbWorker = GetWorkersFromDb(assignment.WorkerId, ownerId);

                var dbJobs = GetJobsFromDb(assignment.Jobs, ownerId);

                var owner = GetOwnerByID(ownerId);

                var insertedEntity = _unitOfWork.Assignments.Insert(new Assignment
                {
                    Worker = dbWorker,
                    Jobs = dbJobs,
                    Wage = assignment.Wage,
                    Owner = owner,
                    AssignedDate = assignment.AssignedDate
                });

                insertedRecords.Add(_mapper.Map<AssignmentDTO>(insertedEntity));
            }

            _unitOfWork.Commit();
            return insertedRecords;
        }

        private void ValidateWorkers(IEnumerable<int> workerIdList, int ownerId)
        {
            foreach (var workerId in workerIdList)
            {
                GetWorkersFromDb(workerId, ownerId);//Check if the worker exists in DB
            }
        }

        private void ValidateJobs(IEnumerable<List<JobDTO>> @select)
        {
            throw new NotImplementedException();
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
            if (worker == null || worker.OwnerId != ownerId) throw new WtException(_strLocalizer["ErrorWorkerNotFound"], Constants.WORKER_NOT_FOUND);
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
            if (jobs == null || jobs.Count == 0) throw new Exception(_strLocalizer["ErrorInvalidJob"]);
            var dbjobs = new List<Job>();

            foreach (var job in jobs)
            {
                var dbjob = _unitOfWork.Jobs.GetByID(job.Id);
                if (dbjob == null || dbjob.OwnerId != ownerId) throw new WtException(_strLocalizer["ErrorInvalidJob"], Constants.JOB_NOT_FOUND);
                dbjobs.Add(dbjob);
            }

            return dbjobs;
        }

        private Owner GetOwnerByID(int ownerId)
        {
            var owner = _unitOfWork.Owners.GetByID(ownerId);
            if (owner == null)
                throw new WtException(_strLocalizer["OwnerNotFound"], Constants.OWNER_NOT_FOUND);

            return owner;
        }
    }
}