﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Localization;
using WorkTracker.Database.DTOs;
using WorkTracker.Database.Interfaces;
using WorkTracker.Database.Models;
using WorkTracker.Server.Services.Contract;

namespace WorkTracker.Server.Services
{
    public class WorkerService : IWorkerService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IHelper _helper;
        private IStringLocalizer _strLocalizer;

        public WorkerService(IUnitOfWork unitOfWork, IStringLocalizer<Resource> stringLocalizer,IMapper mapper, IHelper helper)
        {
            _mapper = mapper;
            _helper = helper;
            _strLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Adds the worker belong to a owner
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="workerName"></param>
        /// <param name="mobileNumber"></param>
        /// <returns></returns>
        public WorkerDTO AddWorkerForOwner(int ownerId, string workerName, string mobileNumber)
        {
            var owner = _unitOfWork.Owners.GetByID(ownerId);
            if (owner == null)
            {
                throw new Exception(_strLocalizer["OwnerNotFound"]);
            }

            _unitOfWork.Workers.Insert(new Worker()
            {
                Owner = owner,
                Name = workerName,
                Mobile = mobileNumber
            });
            _unitOfWork.Commit();

            var dbWorker = _unitOfWork.Workers.Get(x => x.Name == workerName).FirstOrDefault();
            return _mapper.Map<WorkerDTO>(dbWorker);
        }
        
        /// <summary>
        /// Retrieves all the workers belongs to a Owner
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public List<WorkerDTO> GetAllWorkersBelongsToOwner(int ownerId)
        {
            try
            {
                var owner = _unitOfWork.Owners.GetByID(ownerId);
                if (owner == null)
                    throw new Exception(_strLocalizer["OwnerNotFound"]);

                var result = _unitOfWork.Workers.Get(x => x.OwnerId == owner.Id);
               var workerList =  result?.Select(x => new WorkerDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Mobile = x.Mobile,
                    OwnerId = x.OwnerId
                });

               return workerList?.ToList();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
