using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DAL.Interfaces;
using DAL.Models;
using DAL.SQL;
using Dapper;
using Microsoft.Extensions.Options;
using ObjectContracts;
using ObjectContracts.DataTransferObjects;

namespace DAL.Repository
{
    public class ProjectRepo : BaseRepository, IProjectRepo
    {
        public ProjectRepo(IOptions<ConnectionStrings> connectionStrings)
            : base(connectionStrings.Value)
        {
            //_con = connectionStrings.Value.SQLServer;
        }

        public IEnumerable<ProjectDto> GetProject(int projectId)
        {
            var (enumeratedColumnDtos, enumeratedTaskDtos, timeLogDtos, usersDtos, projectDto, usersTasksDtos) =
                ExecuteFunc(con =>
                    {
                        var query = con.QueryMultiple(ProjectSql.GetProject, new {ProjectId = projectId});
                        var timeLogs = query.Read<TimeLogViewDto>();
                        var users = query.Read<UserDto>();
                        var usersTasks = query.Read<UserViewDto>();
                        var tasks = query.Read<TaskViewDto>();
                        var columns = query.Read<ColumnDto>();
                        var projects = query.Read<ProjectViewDto>().First();
                        return (columns.ToList(), tasks.ToList(), timeLogs.ToList(), users.ToList(), projects,
                            usersTasks.ToList());
                    }
                );

            // aint this a mess

            foreach (var columnDto in enumeratedColumnDtos)
            foreach (var taskViewDto in enumeratedTaskDtos)
            {
                var thisTasksTimelogs = new List<TimeLogViewDto>();

                if (columnDto.ColumnId == taskViewDto.userColId || columnDto.ColumnId == taskViewDto.timelogColId)
                {
                    var thisTasksUsers = new List<UserViewDto>();
                    // Get the timelogs related to this task
                    foreach (var timelog in timeLogDtos)
                        if (timelog.linkTimelogTaskId == taskViewDto.TaskId)
                            thisTasksTimelogs.Add(timelog);
                    // Get the users related to this task
                    foreach (var user in usersTasksDtos)
                        if (user.linkUserTaskId == taskViewDto.TaskId)
                            thisTasksUsers.Add(user);

                    var task = new TaskDto(taskViewDto);
                    // Turn timelog views into normal timelog dtos
                    var timelogs = thisTasksTimelogs.Select(t => new TimeLogDto(t)).ToList();
                    var users = thisTasksUsers.Select(u => new UserDto(u)).ToList();
                    // Add the timelogs to the task
                    task.AddTimelog(timelogs);
                    task.AddUser(users);
                    // Add the task to the column
                    columnDto.AddTask(task);
                }
            }

            return new List<ProjectDto> {new ProjectDto(projectDto, usersDtos, enumeratedColumnDtos)};
        }

        public int CreateProject(ProjectDto project)
        {
            var result = ExecuteFunc(con =>
                con.QuerySingleOrDefault<int>(ProjectSql.CreateProject, new
                {
                    project.ProjectName, StartTime = project.ProjectStartTime, EndTime = project.ProjectEndTime,
                    project.ExpectedEndTime,
                    project.PointsTotal, AddedPointsTotal = project.AddedPoints,
                    ProjectComplete = project.ProjectCompleated,
                    project.ProjectArchived,
                    project.TimeIncrement
                }));
            return result;
        }

        public int UpdateProject(string projectName, DateTime startTime, DateTime endTime, DateTime expectedEndTime,
            int pointsTotal, int addedPoints, string projectComplete, string projectArchived, int timeIncrement,
            int projectId)
        {
            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.UpdateProject,
                new
                {
                    ProjectName = projectName,
                    ProjectStartTime = startTime,
                    ProjectEndTime = endTime,
                    ExpectedEndTime = expectedEndTime,
                    PointsTotal = pointsTotal,
                    AddedPoints = addedPoints,
                    ProjectComplete = projectComplete,
                    ProjectArchived = projectArchived,
                    TimeIncrement = timeIncrement,
                    ProjectId = projectId
                }));

            return result;
        }

        public IEnumerable<TaskDto> GetUserTasks(int userId)
        {
            var result = ExecuteFunc(con => con.Query<TaskDto>(ProjectSql.GetUserstasks,
                new
                {
                    UserId = userId
                }));

            return result;
        }

        public int CreateColumn(string columnName, int projectId, int pointsTotal, int addedPointsTotal)
        {
            var dtCol = new DataTable();
            dtCol.Columns.Add("ColumnName");
            dtCol.Columns.Add("PointsTotal");
            dtCol.Columns.Add("AddedPointsTotal");
            dtCol.Rows.Add(columnName);
            dtCol.Rows.Add(pointsTotal);
            dtCol.Rows.Add(addedPointsTotal);

            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.CreateColumn,
                new
                {
                    ColumnName = columnName, PointsTotal = pointsTotal, AddedPointsTotal = addedPointsTotal,
                    ProjectId = projectId, Columns = dtCol.AsTableValuedParameter("TVP_Column")
                }));

            return result;
        }

        public int UpdateColumn(string columnName, int columnId, int pointsTotal, int addedPointsTotal)
        {
            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.UpdateColumn,
                new
                {
                    ColumnName = columnName,
                    PointsTotal = pointsTotal,
                    AddedPointsTotal = addedPointsTotal,
                    ColumnId = columnId
                }));

            return result;
        }

        public int CreateTask(string taskName, string comments, int pointsTotal, int addedPointsTotal,
            DateTime startTime, DateTime endTime, DateTime expectedEndTime, string taskDone, string taskDeleted,
            string taskArchived, string extentionReason, string addedReason, int columnId)
        {
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Comments");
            dt.Columns.Add("PointsTotal");
            dt.Columns.Add("AddedPoints");
            dt.Columns.Add("StartTime");
            dt.Columns.Add("EndTime");
            dt.Columns.Add("ExpectedEndTime");
            dt.Columns.Add("TaskDone");
            dt.Columns.Add("TaskDeleted");
            dt.Columns.Add("TaskArchived");
            dt.Columns.Add("ExtentionReason");
            dt.Columns.Add("AddedReason");
            dt.Rows.Add(taskName, comments, pointsTotal, addedPointsTotal, startTime, endTime, expectedEndTime,
                taskDone, taskDeleted, taskArchived, extentionReason, addedReason);

            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.CreateTask,
                new
                {
                    ColumnId = columnId,
                    Tasks = dt.AsTableValuedParameter("TVP_Task"),
                    TaskName = taskName,
                    Comments = comments,
                    PointsTotal = pointsTotal,
                    AddedPointsTotal = addedPointsTotal,
                    StartTime = startTime,
                    EndTime = endTime,
                    ExpectedEndTime = expectedEndTime,
                    TaskDone = taskDone,
                    TaskDeleted = taskDeleted,
                    TaskArchived = taskArchived,
                    ExtentionReason = extentionReason,
                    AddedReason = addedReason
                }));

            return result;
        }

        public int UpdateTask(string taskName, string comments, int pointsTotal, int addedPointsTotal,
            DateTime startTime, DateTime endTime, DateTime expectedEndTime, string taskDone, string taskDeleted,
            string taskArchived, string extensionReason, string addedReason, int columnId, int taskId)
        {
            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.UpdateTask,
                new
                {
                    TaskName = taskName,
                    Comments = comments,
                    PointsTotal = pointsTotal,
                    AddedPointsTotal = addedPointsTotal,
                    StartTime = startTime,
                    EndTime = endTime,
                    ExpectedEndTime = expectedEndTime,
                    TaskDone = taskDone,
                    TaskDeleted = taskDeleted,
                    TaskArchived = taskArchived,
                    ExtensionReason = extensionReason,
                    AddedReason = addedReason,
                    TaskId = taskId
                }));

            return result;
        }

        public UserDto CreateUser(CreateUser createUser)
        {
            var result = ExecuteFunc(con =>
                con.QuerySingleOrDefault<UserDto>(ProjectSql.CreateUser,
                    new
                    {
                        UserName = createUser.userName, Role = createUser.role, Email = createUser.email,
                        Password = BCrypt.Net.BCrypt.HashPassword(createUser.password), AccessToken = createUser.accessToken,
                        Archived = createUser.archived
                    }));
            return result;
        }

        public int UpdateUser(string userName, string role, string email, string password, string accessToken,
            string archived, int userId)
        {
            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.UpdateUser,
                new
                {
                    UserName = userName,
                    Role = role,
                    Email = email,
                    Password = BCrypt.Net.BCrypt.HashPassword(password),
                    AccessToken = accessToken,
                    Archived = archived,
                    UserId = userId
                }));

            return result;
        }

        public UserDto CheckLogin(string email, string password)
        {
            var result = ExecuteFunc(con => con.QuerySingleOrDefault<UserDto>(ProjectSql.CheckUser,
                new
                {
                    Email = email
                }));
            var check = BCrypt.Net.BCrypt.Verify(password, result.Password);
            if (check) return result;
            {
                return null;
            }
        }

        public int SetTaskUser(int taskId, int userId)
        {
            var dt = new DataTable();
            dt.Columns.Add("TaskId");
            dt.Columns.Add("UserId");
            dt.Rows.Add(taskId, userId);
            var result = ExecuteFunc(con =>
                con.QuerySingleOrDefault<int>(ProjectSql.SetTaskUser, new {UserId = userId, TaskId = taskId}));
            return result;
        }

        public int SetProjectUser(int projectId, int userId)
        {
            var dt = new DataTable();
            dt.Columns.Add("ProjectId");
            dt.Columns.Add("UserId");
            dt.Rows.Add(projectId, userId);
            var result = ExecuteFunc(con =>
                con.QuerySingleOrDefault<int>(ProjectSql.SetProjectUser, new {ProjectId = projectId, UserId = userId}));
            return result;
        }

        public int CreateTimeLog(DateTime startTime, DateTime endTime, float totalTime, string billable,
            string archived, int userId, int taskId)
        {
            var dt = new DataTable();
            dt.Columns.Add("StartTime", typeof(DateTime));
            dt.Columns.Add("EndTime", typeof(DateTime));
            dt.Columns.Add("TotalTime", typeof(float));
            dt.Columns.Add("Billable");
            dt.Columns.Add("Archived");
            dt.Rows.Add(startTime, endTime, totalTime, billable, archived);

            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.CreateTimeLog,
                new {TimeLogs = dt.AsTableValuedParameter("TVP_TimeLogs"), UserId = userId, TaskId = taskId}));

            return result;
        }

        public int UpdateTimeLog(DateTime startTime, DateTime endTime, float totalTime, string billable,
            string archived, int timelogId)
        {
            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.UpdateTimeLog,
                new
                {
                    StartTime = startTime,
                    EndTime = endTime,
                    TotalTime = totalTime,
                    Billable = billable,
                    Archived = archived,
                    TimeLogId = timelogId
                }));

            return result;
        }

        public int DeleteColumn(int columnId)
        {
            return 1;
        }

        public int DeleteTask(int taskId)
        {
            return 1;
        }

        public int DeleteUser(int userId)
        {
            return 1;
        }

        public int DeleteTimeLog(int timelogId)
        {
            return 1;
        }

        //public int Create(ProjectDto project)
        //{
        //    // create data table from tasks
        //    var dt = new DataTable();
        //    dt.Columns.Add("Name");
        //    dt.Columns.Add("Comments");

        //    var dtCol = new DataTable();
        //    dtCol.Columns.Add("ColumnName");

        //    var dtTim = new DataTable();
        //    dtTim.Columns.Add("StartTime");
        //    dtTim.Columns.Add("EndTime");
        //    dtTim.Columns.Add("TotalTime");

        //    var dtUse = new DataTable();
        //    dtUse.Columns.Add("UserName");
        //    dtUse.Columns.Add("Role");

        //    var dltCt = new DataTable();
        //    dltCt.Columns.Add("ColumnId");
        //    dltCt.Columns.Add("TaskId");

        //    var dltPc = new DataTable();
        //    dltPc.Columns.Add("ProjectId");
        //    dltPc.Columns.Add("ColumnId");

        //    var dltTt = new DataTable();
        //    dltTt.Columns.Add("TaskId");
        //    dltTt.Columns.Add("TimeLogId");

        //    var dltUt = new DataTable();
        //    dltUt.Columns.Add("UserId");
        //    dltUt.Columns.Add("TimeLogId");

        //    var dltTu = new DataTable();
        //    dltTu.Columns.Add("TaskId");
        //    dltTu.Columns.Add("UserId");

        //    var dltPu = new DataTable();
        //    dltPu.Columns.Add("ProjectId");
        //    dltPu.Columns.Add("UserId");

        //    foreach (var user in project.Users)
        //    {
        //        dltPu.Rows.Add(project.ProjectId, user.UserId);
        //        dtUse.Rows.Add(user.UserName, user.Role);
        //    }

        //    // Add all the rows to table
        //    foreach (var col in project.Columns)
        //    {
        //        dtCol.Rows.Add(col.ColumnName);
        //        foreach (var task in col.Tasks)
        //        {
        //            dt.Rows.Add(task.TaskName, task.Comments);
        //            foreach (var timelog in task.Timelogs)
        //            {
        //                dtTim.Rows.Add(timelog.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
        //                    timelog.EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), timelog.TotalTime);
        //                dltTt.Rows.Add(task.TaskId, timelog.TimeLogId);
        //            }

        //            foreach (var user in task.Users)
        //            {
        //                dtUse.Rows.Add(user.UserName, user.Role);
        //                dltTu.Rows.Add(task.TaskId, user.UserId);
        //            }

        //            dltCt.Rows.Add(col.ColumnId, task.TaskId);
        //        }

        //        dltPc.Rows.Add(project.ProjectId, col.ColumnId);
        //    }

        //    // Execute the sql, pass in project name and the data table
        //    var result = ExecuteFunc(con => con.Query(ProjectSql.CreateProjectAndTasks,
        //        new
        //        {
        //            project.ProjectName,
        //            Columns = dtCol.AsTableValuedParameter("TVP_Column"),
        //            Tasks = dt.AsTableValuedParameter("TVP_Task"),
        //            Timelogs = dtTim.AsTableValuedParameter("TVP_Timelogs"),
        //            Users = dtUse.AsTableValuedParameter("TVP_User")
        //            //UserTimeLogs = dltUT.AsTableValuedParameter("TVP_UserTimeLog"),
        //            //ColumnTasks = dltCT.AsTableValuedParameter("TVP_ColumnTask"),
        //            //ProjectUsers = dltPU.AsTableValuedParameter("TVP_ProjectUser"),
        //            //TaskTimeLogs = dltTT.AsTableValuedParameter("TVP_TaskTimeLog"),
        //            //UserTasks = dltUT.AsTableValuedParameter("TVP_UserTask")
        //        }));

        //    var res = ExecuteFunc(con => con.Query(ProjectSql.CreatLinks,
        //        new
        //        {
        //            UserTimeLogs = dltUt.AsTableValuedParameter("TVP_UserTimeLog"),
        //            ColumnTasks = dltCt.AsTableValuedParameter("TVP_ColumnTask"),
        //            ProjectUsers = dltPu.AsTableValuedParameter("TVP_ProjectUser"),
        //            TaskTimeLogs = dltTt.AsTableValuedParameter("TVP_TaskTimeLog"),
        //            UserTasks = dltUt.AsTableValuedParameter("TVP_UserTask"),
        //            ProjectColumns = dltPc.AsTableValuedParameter("TVP_ProjectColumns")
        //        }));
        //    // result is the PK of project table
        //    return 1;
        //}
    }
}