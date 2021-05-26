using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class GetBoardByIdRequest
    {
        public string token { get; set;}
        public string boardId { get; set;}
    }
}
