using System;
using System.Collections.Generic;

namespace WorkTracker.Database.Models
{
    public class Assignment
    {
        public Assignment()
        {
            this.Comments = new HashSet<Comment>();
            this.Jobs = new HashSet<Job>();
        }
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public int OwnerId { get; set; }
        public DateTime AssignedDate { get; set; }
        public int Wage { get; set; }
       

        //Navigation property
        public virtual Worker Worker { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
