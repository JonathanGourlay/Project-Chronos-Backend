using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_Chronos_Backend.BusinessObjects;

namespace Project_Chronos_Backend.Models
{
    public class CreateTask
    {
        public string taskName { get; set; }
        public string comments { get; set; }
        public int PointsTotal { get; set; }
        public int AddedPointsTotal { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime ExpectedEndTime { get; set; }
        public string TaskDone { get; set; }
        public string TaskDeleted { get; set; }
        public string TaskArchived { get; set; }
        public string ExtensionReason { get; set; }
        public string AddedReason { get; set; }
        public int columnId { get; set; }
    }
}
