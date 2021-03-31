using System;

namespace DAL.DataTransferObjects
{
    public class UserViewDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public int linkUserTaskId { get; set; }
    }
}