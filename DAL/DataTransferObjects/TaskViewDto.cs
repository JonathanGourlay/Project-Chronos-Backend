using System.Collections.Generic;

namespace DAL.DataTransferObjects
{
    public class TaskViewDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Comments { get; set; }
        public int timelogColId { get; set; }
        public int userColId { get; set; }

        public TaskViewDto() {}
    }
}