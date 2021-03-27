using System;
using System.Collections.Generic;
using DAL.DataTransferObjects;
using Project_Chronos_Backend.Extensions;

namespace Project_Chronos_Backend.BusinessObjects
{
    public class ProjectObject
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public List<UserObject> Users { get; set; }
        public List<ColumnObject> Columns { get; set; }

        public ProjectDto ToDto()
        {
            return new ProjectDto()
            {
                Columns = Columns.ToDto(),
                ProjectName = ProjectName,
                ProjectId = ProjectId,
                Users = Users.ToDto(),
            };
        }
    }

    public class ColumnObject
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public List<TaskObject> Tasks { get; set; }

        public ColumnDto ToDto()
        {
            return new ColumnDto()
            {
                ColumnId = ColumnId,
                ColumnName = ColumnName,
                Tasks = Tasks.ToDto(),
            };
        }
    }

    public class UserObject
    {
        public int PersonId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public UserDto ToDto()
        {
            return new UserDto()
            {
                PersonId = PersonId,
                UserName = UserName,
                Role = Role,
            };
        }
    }
    public class TaskObject
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Comments { get; set; }
        public List<TimeLogObject> Timelogs { get; set; }
        public List<UserObject> Users { get; set; }

        public TaskDto ToDto()
        {
            return new TaskDto()
            {
                TaskId = TaskId,
                TaskName=  TaskName,
                Comments = Comments,
                Timelogs = Timelogs.ToDto(),
                Users = Users.ToDto(),
            };
        }
    }

    public class TimeLogObject
    {
        public int TimeLogId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float TotalTime { get; set; }

        public TimeLogDto ToDto()
        {
            return new TimeLogDto()
            {
                StartTime = StartTime,
                EndTime = EndTime,
                TimeLogId = TimeLogId,
                TotalTime = TotalTime
            };
        }
    }
}
