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
                    var timelogs = thisTasksTimelogs.Select(t => t);
                    var users = thisTasksUsers.Select(u => new UserDto(u)).ToList();
                    // Add the timelogs to the task
                    //task.AddTimelog(timelogs);
                    //task.AddUser(users);
                    // Add the task to the column
                    columnDto.AddTask(task);
                }
            }

            return new List<ProjectDto> {new ProjectDto(projectDto, usersDtos, enumeratedColumnDtos)};
        }

        public int CreateProject(CreateProject project)
        {
            var res = project;
            var result = ExecuteFunc(con =>
                con.QuerySingleOrDefault<int>(ProjectSql.CreateProject, new
                {
                    project.ProjectName, StartTime = project.ProjectStartTime, EndTime = project.ProjectEndTime,
                    project.ExpectedEndTime,
                    project.PointsTotal, AddedPointsTotal = project.AddedPoints,
                    project.ProjectComplete,
                    project.ProjectArchived,
                    project.TimeIncrement
                }));
            return result;
        }

        public IEnumerable<ProjectDto> GetUserProjects(int userId)
        {
            var (enumeratedColumnDtos, enumeratedTaskDtos, timeLogDtos, usersDtos, projectDto, UserViewDtos) =
                ExecuteFunc(con =>
                    {
                        var query = con.QueryMultiple(ProjectSql.GetUserProject, new {UserId = userId});
                        var timeLogs = query.Read<TimeLogViewDto>();
                        var user = query.Read<UserDto>();
                        var usersTasks = query.Read<UserViewDto>();
                        var tasks = query.Read<TaskViewDto>();
                        var columns = query.Read<ColumnDto>();
                        var projects = query.Read<ProjectViewDto>();
                        return (columns.ToList(), tasks.ToList(), timeLogs.ToList(), user.ToList(), projects.ToList(),
                            usersTasks.ToList());
                    }
                );
            var userProjects = new List<ProjectDto>();
            foreach (var project in projectDto)
            {
                var columns = new List<ColumnDto>();
                foreach (var columnDto in enumeratedColumnDtos)
                {
                    if (project.ProjectId == columnDto.ProjectId)
                    {
                        var newColumnDto = columnDto;
                        foreach (var taskViewDto in enumeratedTaskDtos)
                        {

                            if (columnDto.ColumnId == taskViewDto.userColId ||
                                columnDto.ColumnId == taskViewDto.timelogColId)
                            {
                                var thisTasksTimelogs = new List<TimeLogViewDto>();
                                var thisUsers = new List<UserViewDto>();
                                var thisTaskUsers = new List<UserViewDto>();
                                // Get the timelogs related to this task
                                foreach (var timelog in timeLogDtos)

                                    thisTasksTimelogs.Add(timelog);
                                //
                                foreach (var user in UserViewDtos)
                                    thisUsers.Add(user);
                                //

                                var task = new TaskDto(taskViewDto);
                                var timelogs = thisTasksTimelogs.Select(t => t).ToList();
                                var users = thisUsers.Select(u => u).ToList();
                                // Add the timelogs to the task
                                //task.AddTimelog(timelogs);
                                // task.AddUser(users);
                                // Add the task to the column
                                // if null instantie the list
                                if (newColumnDto.Tasks == null)
                                {
                                    newColumnDto.Tasks = new List<TaskDto>();
                                }

                                if (task.Timelogs == null)
                                {
                                    task.Timelogs = new List<TimeLogViewDto>();
                                }

                                foreach (var timelog in timelogs)
                                {
                                    if (timelog.linkTimelogTaskId == task.TaskId)
                                    {
                                        task.Timelogs = task.Timelogs.Append(timelog);
                                    }

                                }

                                if (task.Users == null)
                                {
                                    task.Users = new List<UserViewDto>();
                                }

                                foreach (var user in UserViewDtos)
                                {
                                    if (user.linkUserTaskId == task.TaskId)
                                    {
                                        task.Users = task.Users.Append(user);
                                    }

                                }

                                // append to the list, doesnt break the ienumerable, is just worked out at the next .ToList()
                                newColumnDto.Tasks = newColumnDto.Tasks.Append(task);
                            }
                        }

                        columns.Add(newColumnDto);
                    }
                }
                userProjects.Add(new ProjectDto(project, usersDtos, columns));
            }

            return userProjects;
        }


        public IEnumerable<ProjectDto> GetAdminProjects()
        {
            var (enumeratedColumnDtos, enumeratedTaskDtos, timeLogDtos, usersDtos, projectDto, UserViewDtos) =
                ExecuteFunc(con =>
                {
                    var query = con.QueryMultiple(ProjectSql.GetAdminProjects);
                    var timeLogs = query.Read<TimeLogViewDto>();
                    var user = query.Read<UserDto>();
                    var usersTasks = query.Read<UserViewDto>();
                    var tasks = query.Read<TaskViewDto>();
                    var columns = query.Read<ColumnDto>();
                    var projects = query.Read<ProjectViewDto>();
                    return (columns.ToList(), tasks.ToList(), timeLogs.ToList(), user.ToList(), projects.ToList(),
                        usersTasks.ToList());
                }
                );
            var userProjects = new List<ProjectDto>();
            foreach (var project in projectDto)
            {
                var columns = new List<ColumnDto>();
                foreach (var columnDto in enumeratedColumnDtos)
                {
                    if (project.ProjectId == columnDto.ProjectId)
                    {
                        var newColumnDto = columnDto;
                        foreach (var taskViewDto in enumeratedTaskDtos)
                        {

                            if (columnDto.ColumnId == taskViewDto.userColId ||
                                columnDto.ColumnId == taskViewDto.timelogColId)
                            {
                                var thisTasksTimelogs = new List<TimeLogViewDto>();
                                var thisUsers = new List<UserViewDto>();
                                var thisTaskUsers = new List<UserViewDto>();
                                // Get the timelogs related to this task
                                foreach (var timelog in timeLogDtos)

                                    thisTasksTimelogs.Add(timelog);
                                //
                                foreach (var user in UserViewDtos)
                                    thisUsers.Add(user);
                                //

                                var task = new TaskDto(taskViewDto);
                                var timelogs = thisTasksTimelogs.Select(t => t).ToList();
                                var users = thisUsers.Select(u => u).ToList();
                                // Add the timelogs to the task
                                //task.AddTimelog(timelogs);
                                // task.AddUser(users);
                                // Add the task to the column
                                // if null instantie the list
                                if (newColumnDto.Tasks == null)
                                {
                                    newColumnDto.Tasks = new List<TaskDto>();
                                }

                                if (task.Timelogs == null)
                                {
                                    task.Timelogs = new List<TimeLogViewDto>();
                                }

                                foreach (var timelog in timelogs)
                                {
                                    if (timelog.linkTimelogTaskId == task.TaskId)
                                    {
                                        task.Timelogs = task.Timelogs.Append(timelog);
                                    }

                                }

                                if (task.Users == null)
                                {
                                    task.Users = new List<UserViewDto>();
                                }

                                foreach (var user in UserViewDtos)
                                {
                                    if (user.linkUserTaskId == task.TaskId)
                                    {
                                        task.Users = task.Users.Append(user);
                                    }

                                }

                                // append to the list, doesnt break the ienumerable, is just worked out at the next .ToList()
                                newColumnDto.Tasks = newColumnDto.Tasks.Append(task);
                            }
                        }

                        columns.Add(newColumnDto);
                    }
                }
                userProjects.Add(new ProjectDto(project, usersDtos, columns));
            }

            return userProjects;
        }




        public int UpdateProject(ProjectViewDto project)
        {
            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.UpdateProject,
                new
                {
                    ProjectName = project.ProjectName,
                    project.ProjectStartTime,
                    project.ProjectEndTime,
                    project.ExpectedEndTime,
                    project.PointsTotal,
                    project.AddedPoints,
                    project.ProjectComplete,
                    project.ProjectArchived,
                    project.TimeIncrement,
                    ProjectId = project.ProjectId,

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
                    ColumnName = create.columnName, PointsTotal = create.pointsTotal,
                    AddedPointsTotal = create.addedPointsTotal,
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
            var res = create;
            var result = ExecuteFunc(con => con.QuerySingleOrDefault<int>(ProjectSql.CreateTask,
                new
                {
                    ColumnId = create.columnId,
                    TaskName = create.taskName,
                    Comments = create.comments,
                    create.PointsTotal,
                    create.AddedPointsTotal,
                    create.StartTime,
                    create.EndTime,
                    create.ExpectedEndTime,
                    create.TaskDone,
                    create.TaskDeleted,
                    create.TaskArchived,
                    ExtensionReason = create.ExtensionReason,
                    create.AddedReason
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
                    task.PointsTotal,
                    AddedPointsTotal = task.AddedPoints,
                    task.StartTime,
                    task.EndTime,
                    task.ExpectedEndTime,
                    task.TaskDone,
                    task.TaskDeleted,
                    task.TaskArchived,
                    task.ExtensionReason,
                    task.AddedReason,
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
                        Password = BCrypt.Net.BCrypt.HashPassword(createUser.password),
                        AccessToken = createUser.accessToken,
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
        public int SetColumnTask(int columndId, int taskId)
        {
            var result = ExecuteFunc(con =>
                con.QuerySingleOrDefault<int>(ProjectSql.SetColumnTask, new { ColumnId = columndId, TaskId = taskId }));
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
                new
                {
                    TimeLogs = dt.AsTableValuedParameter("TVP_TimeLogs"), UserId = timelog.userId,
                    TaskId = timelog.taskId
                }));

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
                    TimeLogId = timelog.timelogId,
                    UserId = timelog.userId

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
    }
}