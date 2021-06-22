using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTracker.Database.DTOs;

namespace WorkTracker.Server.Services.Contract
{
    public interface IAssignmentService
    {
        /// <summary>
        /// Insert multiple assignment into the database
        /// </summary>
        /// <param name="assignments"></param>
        /// <returns></returns>
        List<AssignmentDTO> AddAssignments(List<AssignmentDTO> assignments);

        /// <summary>
        /// Returns all the assignment within the given date range corresponding to specific worker and owner.
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="workerId"></param>
        /// <returns></returns>
        List<AssignmentDTO> GetAllAssignments(int ownerId, DateTime startDateTime, DateTime endDateTime, int? workerId, int? jobId);

        /// <summary>
        /// Updates the assignment
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="assignedDate"></param>
        /// <param name="assignmentId"></param>
        /// <param name="wage"></param>
        /// <param name="jobsToUpdate"></param>
        /// <returns></returns>
        bool DeleteAssignments(int ownerId, DateTime assignedDate);

        /// <summary>
        /// Adds single comment to assignment
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        CommentDTO AddComment(int assignmentId, string comment);

        /// <summary>
        /// Returns the assignment by id
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <returns></returns>
        AssignmentDTO GetAssignmentById(int assignmentId);
    }
}
