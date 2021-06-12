using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTracker.Database.DTOs;
using WorkTracker.Database.Models;

namespace WorkTracker.Server.Services.Contract
{
    public interface IPaymentService
    {
        /// <summary>
        /// Retrieves the payments made by/to worker
        /// </summary>
        /// <param name="workerId"></param>
        /// <returns></returns>
        IEnumerable<PaymentDTO> GetAllPayments(int workerId);

        /// <summary>
        /// Insert a payment for a worker
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        PaymentDTO AddPayment(PaymentDTO payment);
    }
}
