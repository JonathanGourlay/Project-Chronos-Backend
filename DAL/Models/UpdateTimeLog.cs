using System;

namespace DAL.Models
{
    public class UpdateTimeLog
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public float totalTime { get; set; }
        public string billable { get; set; }
        public string archived { get; set; }
        public int timelogId { get; set; }
        public int userId { get; set; }
    }
}
