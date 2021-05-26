using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class DeleteCardRequest
    {
        public string token { get; set; }
        public string cardId { get; set; }
    }
}
