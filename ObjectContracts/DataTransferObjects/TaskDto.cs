using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Manatee.Trello;

namespace ObjectContracts.DataTransferObjects
{
    public class TaskDto
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
        public IEnumerable<TimeLogViewDto> Timelogs { get; set; }
        public IEnumerable<UserViewDto> Users { get; set; }
       

        public TaskDto(TaskViewDto dto)
        {
            TaskId = dto.TaskId;
            TrelloTaskId = dto.TrelloTaskId;
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
            Timelogs = new List<TimeLogViewDto>();
            Users = new List<UserViewDto>();
        }

        public void AddTimelog(TimeLogViewDto timelog)
        {
            // for safety as the constructor might not have run
            if (Timelogs == null)
            {
                Timelogs = new List<TimeLogViewDto>();
            }

            Timelogs.Append(timelog);
            //Timelogs.ToList().Add(timelog);
        }

        public void AddTimelog(IEnumerable<TimeLogViewDto> timelogs)
        {
            if (Timelogs == null)
            {
                Timelogs = new List<TimeLogViewDto>();
            }

            foreach (var timelog in timelogs)
            {
                Timelogs.Append(timelog);
            }
            //timelogs.Select(dto => Timelogs.Append(dto));
            //Timelogs.ToList().AddRange(timelogs);
        }
        //public void AddUser(UserDto user)
        //{
        //    // for safety as the constructor might not have run
        //    if (Users == null)
        //    {
        //        Users = new List<UserDto>();
        //    }

        //    Users.ToList().Add(user);
        //}

        //public void AddUser(IEnumerable<UserDto> users)
        //{
        //    if (Users == null)
        //    {
        //        Users = new List<UserDto>();
        //    }

        //    Users.ToList().AddRange(users);
        //}
    }
}