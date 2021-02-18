using System;
using System.Collections.Generic;

namespace WorkTracker.Database.Models
{
    public class Job
    {
        public Job()
        {
            this.Assignments = new HashSet<Assignment>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OwnerId { get; set; }

        //Navigation property
        public virtual Owner Owner { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
