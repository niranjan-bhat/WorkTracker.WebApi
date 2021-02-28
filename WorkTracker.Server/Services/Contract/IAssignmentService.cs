﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTracker.Database.DTOs;

namespace WorkTracker.Server.Services.Contract
{
    public interface IAssignmentService
    {
        /// <summary>
        /// Insert assignment to the database
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="wage"></param>
        /// <param name="workerId"></param>
        /// <param name="assignedDate"></param>
        /// <param name="jobs"></param>
        /// <returns></returns>
        AssignmentDTO AddAssignment(int ownerId, int wage, int workerId, DateTime assignedDate, List<JobDTO> jobs);

        /// <summary>
        /// Returns all the assignment within the given date range corresponding to specific worker and owner.
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="workerId"></param>
        /// <returns></returns>
        List<AssignmentDTO> GetAllAssignments(int ownerId, DateTime startDateTime, DateTime endDateTime, int workerId);

        /// <summary>
        /// Updates the assignment
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="assignment"></param>
        /// <returns></returns>
        AssignmentDTO UpdateAssignment(int ownerId, AssignmentDTO assignment);

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

        /// <summary>
        /// Returns the assignment assigned to this worker on this date
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        AssignmentDTO GetAssignmentForDate(int workerId, DateTime date, int ownerId);

        /// <summary>
        /// Checks if owner submitted the attendance
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        bool IsAssignmentSubmitted(int ownerId, DateTime date);
    }
}
