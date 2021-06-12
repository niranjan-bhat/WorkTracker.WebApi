using WorkTracker.Database.Interfaces;
using WorkTracker.Database.Models;

namespace WorkTracker.Database
{
    public class UnitOfWork : IUnitOfWork
    {


        private WorkTrackerContext _dbContext;
        private BaseRepository<Worker> _workersRepository;
        private BaseRepository<Job> _JobRepository;
        private BaseRepository<Assignment> _assignments;
        private BaseRepository<Comment> _commentRepository;
        private BaseRepository<Owner> _ownerRepository;
        private BaseRepository<Payment> _paymentRepository;

        public UnitOfWork(WorkTrackerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<Worker> Workers
        {
            get
            {
                return _workersRepository ??= new BaseRepository<Worker>(_dbContext);
            }
        }

        public IRepository<Job> Jobs
        {
            get
            {
                return _JobRepository ??= new BaseRepository<Job>(_dbContext);
            }
        }

        public IRepository<Assignment> Assignments
        {
            get
            {
                return _assignments ??= new BaseRepository<Assignment>(_dbContext);
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return _commentRepository ??= new BaseRepository<Comment>(_dbContext);
            }
        }

        public IRepository<Owner> Owners
        {
            get
            {
                return _ownerRepository ??= new BaseRepository<Owner>(_dbContext);
            }
        }

        public IRepository<Payment> Payments
        {
            get
            {
                return _paymentRepository ??= new BaseRepository<Payment>(_dbContext);
            }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
