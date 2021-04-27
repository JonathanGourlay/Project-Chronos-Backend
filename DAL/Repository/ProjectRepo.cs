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

        public int CreateProject(CreateProject project)
        {
            var result = ExecuteFunc(con =>
                con.QuerySingleOrDefault<int>(ProjectSql.CreateProject, new
                {
                    project.ProjectName, StartTime = project.ProjectStartTime, EndTime = project.ProjectEndTime,
                    project.ExpectedEndTime,
                    project.PointsTotal, AddedPointsTotal = project.AddedPoints,
                    ProjectComplete = project.ProjectComplete,
                    project.ProjectArchived,
                    project.TimeIncrement
                }));
            return result;
        }

        public int UpdateProject(UpdateProject project)
        {
            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.UpdateProject,
                new
                {
                    ProjectName = project.projectName,
                    ProjectStartTime = project.ProjectStartTime,
                    ProjectEndTime = project.ProjectEndTime,
                    ExpectedEndTime = project.ExpectedEndTime,
                    PointsTotal = project.PointsTotal,
                    AddedPoints = project.AddedPoints,
                    ProjectComplete = project.ProjectComplete,
                    ProjectArchived = project.ProjectArchived,
                    TimeIncrement = project.TimeIncrement,
                    ProjectId = project.projectId
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

        public int CreateColumn(CreateColumn create)
        {
            var dtCol = new DataTable();
            dtCol.Columns.Add("ColumnName");
            dtCol.Columns.Add("PointsTotal");
            dtCol.Columns.Add("AddedPointsTotal");
            dtCol.Rows.Add(create.columnName);
            dtCol.Rows.Add(create.pointsTotal);
            dtCol.Rows.Add(create.addedPointsTotal);

            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.CreateColumn,
                new
                {
                    ColumnName = create.columnName, PointsTotal = create.pointsTotal, AddedPointsTotal = create.addedPointsTotal,
                    ProjectId = create.projectId, Columns = dtCol.AsTableValuedParameter("TVP_Column")
                }));

            return result;
        }

        public int UpdateColumn(UpdateColumn column)
        {
            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.UpdateColumn,
                new
                {
                    ColumnName = column.columnName,
                    PointsTotal = column.pointsTotal,
                    AddedPointsTotal = column.addedPointsTotal,
                    ColumnId = column.columnId
                }));

            return result;
        }

        public int CreateTask(CreateTask create)
        {
            
            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.CreateTask,
                new
                {
                    ColumnId = create.columnId,
                    TaskName = create.taskName,
                    Comments = create.comments,
                    PointsTotal = create.PointsTotal,
                    AddedPointsTotal = create.AddedPointsTotal,
                    StartTime = create.StartTime,
                    EndTime = create.EndTime,
                    ExpectedEndTime = create.ExpectedEndTime,
                    TaskDone = create.TaskDone,
                    TaskDeleted = create.TaskDeleted,
                    TaskArchived = create.TaskArchived,
                    ExtensionReason = create.ExtensionReason,
                    AddedReason = create.AddedReason
                }));

            return result;
        }

        public int UpdateTask(UpdateTask task)
        {
            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.UpdateTask,
                new
                {
                    TaskName = task.taskName,
                    Comments = task.comments,
                    PointsTotal = task.PointsTotal,
                    AddedPointsTotal = task.AddedPoints,
                    StartTime = task.StartTime,
                    EndTime = task.EndTime,
                    ExpectedEndTime = task.ExpectedEndTime,
                    TaskDone = task.TaskDone,
                    TaskDeleted = task.TaskDeleted,
                    TaskArchived = task.TaskArchived,
                    ExtensionReason = task.ExtensionReason,
                    AddedReason = task.AddedReason,
                    TaskId = task.taskId
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

        public int UpdateUser(UpdateUser user)
        {
            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.UpdateUser,
                new
                {
                    UserName = user.userName,
                    Role = user.role,
                    Email = user.email,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.password),
                    AccessToken = user.accessToken,
                    Archived = user.archived,
                    UserId = user.userId
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

        public int CreateTimeLog(CreateTimeLog timelog)
        {
            var dt = new DataTable();
            dt.Columns.Add("StartTime", typeof(DateTime));
            dt.Columns.Add("EndTime", typeof(DateTime));
            dt.Columns.Add("TotalTime", typeof(float));
            dt.Columns.Add("Billable");
            dt.Columns.Add("Archived");
            dt.Rows.Add(timelog.startTime, timelog.endTime, timelog.totalTime, timelog.billable, timelog.archived);

            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.CreateTimeLog,
                new {TimeLogs = dt.AsTableValuedParameter("TVP_TimeLogs"), UserId = timelog.userId, TaskId = timelog.taskId }));

            return result;
        }

        public int UpdateTimeLog(UpdateTimeLog timelog)
        {
            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.UpdateTimeLog,
                new
                {
                    StartTime = timelog.startTime,
                    EndTime = timelog.endTime,
                    TotalTime = timelog.totalTime,
                    Billable = timelog.billable,
                    Archived = timelog.archived,
                    TimeLogId = timelog.timelogId
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