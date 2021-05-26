using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ObjectContracts.DataTransferObjects;

namespace DAL.Models
{
    public class AddCardRequest
    {
        public string token { get; set; }
        public string listId { get; set; }
        public string position { get; set; }
        public TaskDto task { get; set; }
    }
}
