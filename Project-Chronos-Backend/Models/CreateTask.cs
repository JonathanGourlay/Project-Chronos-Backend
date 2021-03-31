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
        public int columnId { get; set; }
    }
}
