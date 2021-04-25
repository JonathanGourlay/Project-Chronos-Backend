using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Chronos_Backend.Models
{
    public class CreateColumn
    {
        public string columnName { get; set; }
        public int projectId { get; set; }
        public int pointsTotal { get; set; }
        public int addedPointsTotal { get; set; }
    }
}
