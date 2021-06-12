using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Database.Models;

namespace WorkTracker.Database.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Worker> Workers { get; }
        IRepository<Job> Jobs { get; }
        IRepository<Assignment> Assignments { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Owner> Owners { get; }
        IRepository<Payment> Payments { get; }
        void Commit();
    }
}
