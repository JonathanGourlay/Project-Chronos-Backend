using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Extensions;
using ObjectContracts.DataTransferObjects;

namespace BLL.BusinessObjects
{
    public class ProjectObject
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime ProjectStartTime { get; set; }
        public DateTime ProjectEndTime { get; set; }
        public DateTime ExpectedEndTime { get; set; }
        public int PointsTotal { get; set; }
        public int AddedPoints { get; set; }
        public string ProjectComplete { get; set; }
        public string ProjectArchived { get; set; }
        public int TimeIncrement { get; set; }
        public List<UserObject> Users { get; set; }
        public List<ColumnObject> Columns { get; set; }


        public ProjectObject() { }

        public ProjectObject(ProjectDto dto)
        {
            ProjectId = dto.ProjectId;
            ProjectName = dto.ProjectName;
            ProjectStartTime = dto.ProjectStartTime;
            ProjectEndTime = dto.ProjectEndTime;
            ExpectedEndTime = dto.ExpectedEndTime;
            PointsTotal = dto.PointsTotal;
            AddedPoints = dto.AddedPoints;
            ProjectComplete = dto.ProjectComplete;
            ProjectArchived = dto.ProjectArchived;
            TimeIncrement = dto.TimeIncrement;
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
                ProjectStartTime = ProjectStartTime,
                ProjectEndTime = ProjectEndTime,
                ExpectedEndTime = ExpectedEndTime,
                PointsTotal = PointsTotal,
                AddedPoints = AddedPoints,
                ProjectComplete = ProjectComplete,
                ProjectArchived = ProjectArchived,
                TimeIncrement = TimeIncrement,
            Users = Users.ToDto(),
            };
        }

    }

    public class ColumnObject
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int PointsTotal { get; set; }
        public int AddedPoints { get; set; }
        public List<TaskObject> Tasks { get; set; }

        public ColumnObject() {}

        public ColumnObject(ColumnDto dto)
        {
            ColumnId = dto.ColumnId;
            ColumnName = dto.ColumnName;
            PointsTotal = dto.PointsTotal;
            AddedPoints = dto.AddedPoints;
            Tasks = dto.Tasks.Select(t => new TaskObject(t)).ToList();
        }

        public ColumnDto ToDto() => new ColumnDto()
        {
            ColumnId = ColumnId,
            ColumnName = ColumnName,
            PointsTotal = PointsTotal,
            AddedPoints = AddedPoints,
            Tasks = Tasks.ToDto(),
        };
    }

    public class LoginObject
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserObject
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public string Archived { get; set; }

        public UserObject() {}

        public UserObject(UserDto dto)
        {
            UserId = dto.UserId;
            UserName = dto.UserName;
            Email = dto.Email;
            Password = dto.Password;
            AccessToken = dto.AccessToken;
            Archived = dto.Archived;
            Role = dto.Role;
        }

        public UserDto ToDto()
        {
            return new UserDto()
            {
                UserId = UserId,
                UserName = UserName,
                Role = Role,
                Email = Email,
                Password = Password,
                AccessToken = AccessToken
            };
        }
    }
    public class TaskObject
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
        public  string TaskArchived { get; set; }
        public string ExtensionReason { get; set; }
        public string AddedReason { get; set; }
        public List<TimeLogObject> Timelogs { get; set; }
        public List<UserObject> Users { get; set; }

        public TaskObject() {}

        public TaskObject(TaskDto dto)
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
                Points = Points,
                AddedPoints = AddedPoints,
                StartTime = StartTime,
                EndTime = EndTime,
                ExpectedEndTime = ExpectedEndTime,
                TaskDone = TaskDone,
                TaskDeleted = TaskDeleted,
                TaskArchived = TaskArchived,
                ExtensionReason = ExtensionReason,
                AddedReason = AddedReason,
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
        public string Billable { get; set; }
        public string Archived { get; set; }

        public TimeLogObject()
        {

        }

        public TimeLogObject(TimeLogDto dto)
        {
            TimeLogId = dto.TimeLogId;
            StartTime = dto.StartTime;
            EndTime = dto.EndTime;
            TotalTime = dto.TotalTime;
            Billable = dto.Billable;
            Archived = dto.Archived;
        }

        public TimeLogDto ToDto()
        {
            return new TimeLogDto()
            {
                StartTime = StartTime,
                EndTime = EndTime,
                TimeLogId = TimeLogId,
                TotalTime = TotalTime,
                Billable = Billable,
                Archived = Archived,
            };
        }
    }
}
