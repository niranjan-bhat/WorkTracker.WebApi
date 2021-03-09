using System;
using System.Collections.Generic;
using System.Text;

namespace WorkTracker.Database.Models
{
    public class Owner
    {
        public Owner()
        {
            this.Jobs = new HashSet<Job>();
            this.Workers = new HashSet<Worker>();
            this.Assignment = new HashSet<Assignment>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string EncryptedPassword { get; set; }

        public bool IsEmailVerified { get; set; }


        //Navigation properties
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Worker> Workers { get; set; }
        public virtual ICollection<Assignment> Assignment { get; set; }
    }
}
