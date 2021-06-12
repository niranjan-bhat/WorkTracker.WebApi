using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PaymentService : IPaymentService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IHelper _helper;
        private IStringLocalizer _strLocalizer;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper, IHelper helper, IStringLocalizer<Resource> stringLocalizer)
        {
            _mapper = mapper;
            _helper = helper;
            _unitOfWork = unitOfWork;
            _strLocalizer = stringLocalizer;
        }

        public IEnumerable<PaymentDTO> GetAllPayments(int workerId)
        {
            var worker = _unitOfWork.Workers.GetByID(workerId);
            if (worker == null)
            {
                throw new WtException(_strLocalizer["ErrorWorkerNotFound"], Constants.WORKER_NOT_FOUND);
            }

            var payments = _unitOfWork.Payments.Get(x => x.WorkerId == workerId);
            return _mapper.Map<IEnumerable<PaymentDTO>>(payments);
        }

        public PaymentDTO AddPayment(PaymentDTO payment)
        {
            var worker = _unitOfWork.Workers.GetByID(payment.WorkerId);
            if (worker == null)
            {
                throw new WtException(_strLocalizer["ErrorWorkerNotFound"], Constants.WORKER_NOT_FOUND);
            }

            var result = _unitOfWork.Payments.Insert(new Payment()
            {
                Worker = worker,
                Amount = payment.Amount,
                TransactionDate = DateTime.Now,
                PaymentType = payment.PaymentType
            });
            _unitOfWork.Commit();

            return _mapper.Map<PaymentDTO>(result);
        }
    }
}
