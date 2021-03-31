using System;

namespace DAL.DataTransferObjects
{
    public class TimeLogViewDto
    {
        public int TimeLogId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float TotalTime { get; set; }
        public int linkTimelogTaskId { get; set; }
    }
}