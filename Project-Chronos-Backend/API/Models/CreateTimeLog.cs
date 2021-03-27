using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_Chronos_Backend.Objects;

namespace Project_Chronos_Backend.API.Models
{
    public class CreateTimeLog
    {
        public TimeLogObject timeLog { get; set; }
        public int userId { get; set; }
        public int taskId { get; set; }
    }
}
