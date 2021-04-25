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
        public string email { get; set; }
        public string password { get; set; }
        public string accessToken { get; set; }
        public string archived { get; set; }

        public int userId { get; set; }
    }
}
