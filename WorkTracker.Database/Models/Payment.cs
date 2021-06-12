using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Database.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime TransactionDate { get; set; }

        public PaymentType PaymentType { get; set; }

        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
    }
}
