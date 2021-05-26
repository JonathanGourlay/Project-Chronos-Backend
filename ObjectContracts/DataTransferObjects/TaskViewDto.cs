using System;
using System.ComponentModel.DataAnnotations;
using Manatee.Trello;

namespace ObjectContracts.DataTransferObjects
{
    public class TaskViewDto
    {
        public int TaskId { get; set; }
        [Required]
        public string TrelloTaskId { get; set; }
        public string TaskName { get; set; }
        public string Comments { get; set; }
        public int Points { get; set; }
        public int AddedPoints { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime ExpectedEndTime { get; set; }
        public string TaskDone { get; set; }
        public string TaskDeleted { get; set; }
        public string TaskArchived { get; set; }
        public string ExtensionReason { get; set; }
        public string AddedReason { get; set; }
        public int timelogColId { get; set; }
        public int userColId { get; set; }

        public TaskViewDto() {}
    }
}