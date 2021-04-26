using System;
using System.Collections.Generic;

namespace DLL.DataTransferObjects
{
    public class TaskDto
    {
        public int TaskId { get; set; }
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
        public List<TimeLogDto> Timelogs { get; set; }
        public List<UserDto> Users { get; set; }

        public TaskDto(TaskViewDto dto)
        {
            TaskId = dto.TaskId;
            TaskName = dto.TaskName;
            Comments = dto.Comments;
            Points = dto.Points;
            AddedPoints = dto.AddedPoints;
            StartTime = dto.StartTime;
            EndTime = dto.EndTime;
            ExpectedEndTime = dto.ExpectedEndTime;
            TaskDone = dto.TaskDone;
            TaskDeleted = dto.TaskDeleted;
            TaskArchived = dto.TaskArchived;
            ExtensionReason = dto.ExtensionReason;
            AddedReason = dto.AddedReason;
        }

        public TaskDto()
        {
            Timelogs = new List<TimeLogDto>();
            Users = new List<UserDto>();
        }

        public void AddTimelog(TimeLogDto timelog)
        {
            // for safety as the constructor might not have run
            if (Timelogs == null)
            {
                Timelogs = new List<TimeLogDto>();
            }

            Timelogs.Add(timelog);
        }

        public void AddTimelog(IEnumerable<TimeLogDto> timelogs)
        {
            if (Timelogs == null)
            {
                Timelogs = new List<TimeLogDto>();
            }
            
            Timelogs.AddRange(timelogs);
        }
        public void AddUser(UserDto user)
        {
            // for safety as the constructor might not have run
            if (Users == null)
            {
                Users = new List<UserDto>();
            }

            Users.Add(user);
        }

        public void AddUser(IEnumerable<UserDto> users)
        {
            if (Users == null)
            {
                Users = new List<UserDto>();
            }

            Users.AddRange(users);
        }
    }
}