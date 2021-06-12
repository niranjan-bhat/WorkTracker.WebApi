using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Database.Models;

namespace WorkTracker.Database.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime TransactionDate { get; set; }

        public PaymentType PaymentType { get; set; }

        public int WorkerId { get; set; }
    }
}
