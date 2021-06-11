using System;

namespace ObjectContracts.DataTransferObjects
{
    public class ProjectViewDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime ProjectStartTime { get; set; }
        public DateTime ProjectEndTime { get; set; }
        public DateTime ExpectedEndTime { get; set; }
        public int PointsTotal { get; set; }
        public int AddedPoints { get; set; }
        public float PointsAchived { get; set; }
        public string ProjectComplete { get; set; }
        public string ProjectArchived { get; set; }
        public int TimeIncrement { get; set; }
    }
}
