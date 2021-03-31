using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Chronos_Backend.Models
{
    public class UpdateTask
    {
        public int taskId { get; set; }
        public string taskName { get; set; }
        public string comments { get; set; }
    }
}
