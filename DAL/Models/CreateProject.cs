using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class CreateProject
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
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
