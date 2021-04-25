using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Chronos_Backend.Models
{
    public class UpdateProject
    {
        public string projectName { get; set; }
        public int projectId { get; set; }
        public DateTime ProjectStartTime { get; set; }
        public DateTime ProjectEndTime { get; set; }
        public DateTime ExpectedEndTime { get; set; }
        public int PointsTotal { get; set; }
        public int AddedPoints { get; set; }
        public string ProjectComplete { get; set; }
        public string ProjectArchived { get; set; }
        public int TimeIncrement { get; set; }
    }
}
