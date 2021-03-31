using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


        public ProjectObject() { }

        public ProjectObject(ProjectDto dto)
        {
            ProjectId = dto.ProjectId;
            ProjectName = dto.ProjectName;
            Users = dto.Users.Select(u => new UserObject(u)).ToList();
            Columns = dto.Columns.Select(c => new ColumnObject(c)).ToList();
        }

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

        public ColumnObject() {}

        public ColumnObject(ColumnDto dto)
        {
            ColumnId = dto.ColumnId;
            ColumnName = dto.ColumnName;
            Tasks = dto.Tasks.Select(t => new TaskObject(t)).ToList();
        }

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
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public UserObject() {}

        public UserObject(UserDto dto)
        {
            UserId = dto.UserId;
            UserName = dto.UserName;
            Role = dto.Role;
        }

        public UserDto ToDto()
        {
            return new UserDto()
            {
                UserId = UserId,
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

        public TaskObject() {}

        public TaskObject(TaskDto dto)
        {
            TaskId = dto.TaskId;
            TaskName = dto.TaskName;
            Comments = dto.Comments;
            Timelogs = dto.Timelogs.Select(tl => new TimeLogObject(tl)).ToList();
            Users = dto.Users.Select(me => new UserObject(me)).ToList();
        }

        public TaskDto ToDto()
        {
            return new TaskDto()
            {
                TaskId = TaskId,
                TaskName=  TaskName,
                Comments = Comments,
                //Timelogs = Timelogs.ToDto(),
                //Users = Users.ToDto(),
            };
        }
    }

    public class TimeLogObject
    {
        public int TimeLogId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float TotalTime { get; set; }

        public TimeLogObject()
        {

        }

        public TimeLogObject(TimeLogDto dto)
        {
            TimeLogId = dto.TimeLogId;
            StartTime = dto.StartTime;
            EndTime = dto.EndTime;
            TotalTime = dto.TotalTime;
        }

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
