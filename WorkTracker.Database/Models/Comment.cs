using System;

namespace WorkTracker.Database.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string OwnerComment { get; set; }
        public int AssignmentId { get; set; }

        public DateTime AddeTime { get; set; }

        public virtual Assignment Assignment { get; set; }  
    }
}
