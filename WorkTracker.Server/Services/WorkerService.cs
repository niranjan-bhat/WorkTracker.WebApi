using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Localization;
using WorkTracker.Database.DTOs;
using WorkTracker.Database.Interfaces;
using WorkTracker.Database.Models;
using WorkTracker.Server.Exceptions;
using WorkTracker.Server.Services.Contract;

namespace WorkTracker.Server.Services
{
    public class WorkerService : IWorkerService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IHelper _helper;
        private IStringLocalizer _strLocalizer;

        public WorkerService(IUnitOfWork unitOfWork, IStringLocalizer<Resource> stringLocalizer, IMapper mapper, IHelper helper)
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
                throw new WtException(_strLocalizer["OwnerNotFound"], Constants.OWNER_NOT_FOUND);
            }

            _unitOfWork.Workers.Insert(new Worker()
            {
                Owner = owner,
                Name = workerName,
                Mobile = mobileNumber
            });

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                var message = e.InnerException?.Message;

                if (message != null && message.Contains(_strLocalizer["DBErrorDuplicateWorkerMobile"]))
                {
                    throw new WtException(_strLocalizer["DuplicateMobileNumber"], Constants.DUPLICATE_MOBILE_NUMBER);
                }

                if (message != null && message.Contains(_strLocalizer["DBErrorDuplicateWorkerName"]))
                {
                    throw new WtException(_strLocalizer["DuplicateWorkerName"], Constants.DUPLICATE_WORKERNAME);
                }

                throw new Exception(message);
            }

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
                    throw new WtException(_strLocalizer["OwnerNotFound"], Constants.OWNER_NOT_FOUND);

                var result = _unitOfWork.Workers.Get(x => x.OwnerId == owner.Id);
                var workerList = result?.Select(x => new WorkerDTO()
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

        public int CalculateSalary(int workerId, DateTime fromDate, DateTime toDate)
        {
            var worker = _unitOfWork.Workers.GetByID(workerId);
            if (worker == null)
                throw new WtException(_strLocalizer["ErrorWorkerNotFound"], Constants.WORKER_NOT_FOUND);

            if ((toDate - fromDate).TotalDays > 1000)
            {
                throw new Exception(_strLocalizer["ErrorOverflowDateRange1000Days"]);
            }

            var allAssignments = _unitOfWork.Assignments.Get(x =>
                                                                  x.WorkerId == workerId &&
                                                                  x.AssignedDate >= fromDate &&
                                                                  x.AssignedDate <= toDate);

            int? totalSalary = allAssignments?.Sum(x => x.Wage);
            return totalSalary.HasValue ? totalSalary.Value : 0;
        }
    }
}
