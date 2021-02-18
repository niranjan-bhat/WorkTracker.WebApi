using System;
using System.Collections.Generic;
using System.Text;
using WorkTracker.Database.DTOs;

namespace WorkTracker.Database.DTO
{
    public class OwnerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
