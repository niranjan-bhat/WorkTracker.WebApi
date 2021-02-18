using System;
using System.Collections.Generic;

namespace WorkTracker.Database.Models
{
    public class Worker
    {
        public Worker()
        {
            this.Assignments = new HashSet<Assignment>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OwnerId { get; set; }
        
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual Owner Owner { get; set; }
    }
}
