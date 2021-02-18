using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkTracker.Server.QueryFilters
{
    public class AssignmentFilterModel
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int WorkerId { get; set; }
    }
}
