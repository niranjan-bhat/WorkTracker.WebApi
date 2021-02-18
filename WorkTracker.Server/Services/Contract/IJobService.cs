using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTracker.Database.DTOs;

namespace WorkTracker.Server.Services.Contract
{
    public interface IJobService
    {
        /// <summary>
        /// Returns all the jobs created by the given owner
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        List<JobDTO> GetAllJobsBelongsToOwner(int ownerId);

        /// <summary>
        /// Adds the job for a owner
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        JobDTO AddJobForOwner(int ownerId, string jobName);
    }
}
