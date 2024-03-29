﻿using System;
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
    public class JobService : IJobService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IHelper _helper;
        private IStringLocalizer _strLocalizer;

        public JobService(IUnitOfWork unitOfWork, IMapper mapper, IHelper helper, IStringLocalizer<Resource> stringLocalizer)
        {
            _mapper = mapper;
            _helper = helper;
            _unitOfWork = unitOfWork;
            _strLocalizer = stringLocalizer;
        }

        /// <summary>
        /// Returns all the jobs created by the given owner
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public List<JobDTO> GetAllJobsBelongsToOwner(int ownerId)
        {
            var owner = _unitOfWork.Owners.GetByID(ownerId);
            if (owner == null)
            {
                throw new Exception(_strLocalizer["OwnerNotFound"]);
            }

            var dBjobs = _unitOfWork.Jobs.Get(x => x.OwnerId == ownerId);
            var jobDto = dBjobs?.Select(x => _mapper.Map<JobDTO>(x));

            return jobDto?.ToList();
        }

        /// <summary>
        /// Adds the job for a owner
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public JobDTO AddJobForOwner(int ownerId, string jobName)
        {
            var owner = _unitOfWork.Owners.GetByID(ownerId);
            if (owner == null)
            {
                throw new Exception(_strLocalizer["OwnerNotFound"]);
            }
            try
            {
                var job = new Job()
                {
                    Name = jobName
                };
                job.Owner = owner;
                _unitOfWork.Jobs.Insert(job);
                _unitOfWork.Commit();

                return _mapper.Map<JobDTO>(job);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
