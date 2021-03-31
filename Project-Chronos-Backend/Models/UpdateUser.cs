using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Project_Chronos_Backend.Models
{
    public class UpdateUser
    {
        public string userName { get; set; }
        public string role { get; set; }
        public int userId { get; set; }
    }
}
