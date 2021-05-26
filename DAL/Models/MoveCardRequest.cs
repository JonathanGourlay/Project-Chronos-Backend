using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectContracts.DataTransferObjects
{
   public class MoveCardRequest
    {
        public string token { get; set; }
        public string cardId { get; set; }
        public string newPosition { get; set; }
        public string boardId { get; set; }
    }
}
