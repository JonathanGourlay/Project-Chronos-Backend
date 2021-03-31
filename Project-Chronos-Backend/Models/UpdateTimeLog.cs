using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Chronos_Backend.Models
{
    public class UpdateTimeLog
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public float totalTime { get; set; }
        public int timelogId { get; set; }
    }
}
