﻿using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Database.DTO;
using WorkTracker.Database.Models;

namespace WorkTracker.Database.DTOs
{
    public class AssignmentDTO
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public DateTime AssignedDate { get; set; }
        public int Wage { get; set; }

        public int OwnerId { get; set; }

        public WorkerDTO Worker { get; set; }
        public List<CommentDTO> Comments { get; set; }
        public List<JobDTO> Jobs { get; set; }
        public virtual OwnerDTO Owner { get; set; }
    }
}
