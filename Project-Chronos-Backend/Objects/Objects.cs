using System;
using System.Collections.Generic;

namespace Project_Chronos_Backend.Objects
{
    public class ProjectObject
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public List<UserObject> Users { get; set; }
        public List<ColumnObject> Columns { get; set; }
    }

    public class ColumnObject
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public List<TaskObject> Tasks { get; set; }
    }

    public class UserObject
    {
        public int PersonId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
    public class TaskObject
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Comments { get; set; }
        public List<TimeLogObject> Timelogs { get; set; }
        public List<UserObject> Users { get; set; }
    }

    public class TimeLogObject
    {
        public int TimeLogId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float TotalTime { get; set; }
    }
}
