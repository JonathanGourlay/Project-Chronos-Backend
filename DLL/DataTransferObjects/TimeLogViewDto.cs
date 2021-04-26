using System;

namespace DLL.DataTransferObjects
{
    public class TimeLogViewDto
    {
        public int TimeLogId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float TotalTime { get; set; }
        public string Billable { get; set; }
        public string Archived { get; set; }
        public int linkTimelogTaskId { get; set; }
    }
}