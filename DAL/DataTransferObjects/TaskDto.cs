using System.Collections.Generic;

namespace DAL.DataTransferObjects
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Comments { get; set; }
        public List<TimeLogDto> Timelogs { get; set; }
        public List<UserDto> Users { get; set; }
    }
}