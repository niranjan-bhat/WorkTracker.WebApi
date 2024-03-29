﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTracker.Database.DTOs;

namespace WorkTracker.Server.Services.Contract
{
    public interface IWorkerService
    {
        /// <summary>
        /// Adds the worker belong to a owner
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="workerName"></param>
        /// <param name="mobileNumber"></param>
        /// <returns></returns>
        WorkerDTO AddWorkerForOwner(int ownerId, string workerName, string mobileNumber);

        /// <summary>
        /// Retrieves all the workers belongs to a Owner
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        List<WorkerDTO> GetAllWorkersBelongsToOwner(int ownerId);
    }
}
