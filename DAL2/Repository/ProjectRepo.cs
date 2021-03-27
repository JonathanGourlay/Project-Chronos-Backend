using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using Project_Chronos_Backend.DAL.DataTransferObjects;
using Project_Chronos_Backend.DAL.Interfaces;
using Project_Chronos_Backend.DAL.SQL;
using Project_Chronos_Backend.Objects;
using ProjectChronosBackend.DAL;

namespace Project_Chronos_Backend.DAL.Repository
{
    public class ProjectRepo: BaseRepository, IProjectRepo
    {
        //private string _con;

        public ProjectRepo(IOptions<ConnectionStrings> connectionStrings)
            : base(connectionStrings.Value)
        {
            //_con = connectionStrings.Value.SQLServer;
        }

        public IEnumerable<ProjectTaskDto> GetProjects(IEnumerable<int> projectIds)
        {
            return ExecuteFunc((con) =>
                con.Query<ProjectTaskDto>(ProjectSql.GetProjectAndTasks, new { ProjectIds = projectIds }));
        }
        public IEnumerable<ProjectObject> GetProject(int projectId)
        {
            return ExecuteFunc((con) =>
                con.Query<ProjectObject>(ProjectSql.GetProject, new { ProjectId = projectId }));
        }

        public int CreateProject(IEnumerable<string> projectName)
        {
            var result =  ExecuteFunc((con) => con.QuerySingleOrDefault<int>(ProjectSql.CreateProject,  new{ProjectName = projectName}));

            return result;
        }
        public int CreateColumn(IEnumerable<string> columnName, int projectId)
        {
            var dtCol = new DataTable();
            dtCol.Columns.Add("ColumnName");
            dtCol.Rows.Add(columnName);
            
            var result = ExecuteFunc((con) => con.QuerySingleOrDefault<int>(ProjectSql.CreateColumn, new { ColumnName = columnName, ProjectId = projectId, Columns = dtCol.AsTableValuedParameter("TVP_Column") }));

            return result;
        }

        public int CreateTask(string taskName, string comments, int columnId)
        {
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Comments");
            dt.Rows.Add(taskName,comments);

            var result = ExecuteFunc((con) => con.QuerySingleOrDefault<int>(ProjectSql.CreateTask, new {ColumnId = columnId, Tasks = dt.AsTableValuedParameter("TVP_Task"), TaskName = taskName, Comments = comments }));

            return result;
        }
        public int CreateUser(string userName, string role)
        {
            var dt = new DataTable();
            dt.Columns.Add("UserName");
            dt.Columns.Add("Role");
            dt.Rows.Add(userName, role);
            var result = ExecuteFunc((con) => con.QuerySingleOrDefault<int>(ProjectSql.CreateUser, new { UserName = userName, Role = role }));
            return result;
        }
        public int SetTaskUser(int taskId, int userId)
        {
            var dt = new DataTable();
            dt.Columns.Add("TaskId");
            dt.Columns.Add("UserId");
            dt.Rows.Add(taskId, userId);
            var result = ExecuteFunc((con) => con.QuerySingleOrDefault<int>(ProjectSql.SetTaskUser, new { UserId = userId, TaskId = taskId }));
            return result;
        }
        public int SetProjectUser(int projectId, int userId)
        {
            var dt = new DataTable();
            dt.Columns.Add("ProjectId");
            dt.Columns.Add("UserId");
            dt.Rows.Add(projectId, userId);
            var result = ExecuteFunc((con) => con.QuerySingleOrDefault<int>(ProjectSql.SetProjectUser, new { ProjectId = projectId, UserId = userId }));
            return result;
        }

        public int CreateTimeLog(DateTime startTime, DateTime endTime, float totalTime, int userId, int taskId)
        {
            var dt = new DataTable();
            dt.Columns.Add("StartTime", typeof(DateTime));
            dt.Columns.Add("EndTime", typeof(DateTime));
            dt.Columns.Add("TotalTime", typeof(float));
            dt.Rows.Add(startTime, endTime, totalTime);

            var result = ExecuteFunc((con) => con.QuerySingleOrDefault<int>(ProjectSql.CreateTimeLog, new { TimeLogs = dt.AsTableValuedParameter("TVP_TimeLogs"), UserId = userId, TaskId = taskId }));

            return result;
        }
        public int Create(ProjectObject project)
        {
            // create data table from tasks
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Comments");

            var dtCol = new DataTable();
            dtCol.Columns.Add("ColumnName");

            var dtTim = new DataTable();
            dtTim.Columns.Add("StartTime");
            dtTim.Columns.Add("EndTime");
            dtTim.Columns.Add("TotalTime");

            var dtUse = new DataTable();
            dtUse.Columns.Add("UserName");
            dtUse.Columns.Add("Role");

            var dltCt = new DataTable();
            dltCt.Columns.Add("ColumnId");
            dltCt.Columns.Add("TaskId");

            var dltPc = new DataTable();
            dltPc.Columns.Add("ProjectId");
            dltPc.Columns.Add("ColumnId");

            var dltTt = new DataTable();
            dltTt.Columns.Add("TaskId");
            dltTt.Columns.Add("TimeLogId");

            var dltUt = new DataTable();
            dltUt.Columns.Add("UserId");
            dltUt.Columns.Add("TimeLogId");

            var dltTu = new DataTable();
            dltTu.Columns.Add("TaskId");
            dltTu.Columns.Add("UserId");

            var dltPu = new DataTable();
            dltPu.Columns.Add("ProjectId");
            dltPu.Columns.Add("UserId");

            foreach (var user in project.Users)
            {
                dltPu.Rows.Add(project.ProjectId, user.PersonId);
                dtUse.Rows.Add(user.UserName, user.Role);
            }

            // Add all the rows to table
            foreach (var col in project.Columns)
            {
                dtCol.Rows.Add(col.ColumnName);
                foreach (var task in col.Tasks)
                {
                    dt.Rows.Add(task.TaskName, task.Comments);
                    foreach (var timelog in task.Timelogs)
                    {
                        dtTim.Rows.Add(timelog.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), timelog.EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), timelog.TotalTime);
                        dltTt.Rows.Add(task.TaskId, timelog.TimeLogId);
                    }

                    foreach (var user in task.Users)
                    {
                        dtUse.Rows.Add(user.UserName, user.Role);
                        dltTu.Rows.Add(task.TaskId, user.PersonId);
                    }

                    dltCt.Rows.Add(col.ColumnId, task.TaskId);
                }

                dltPc.Rows.Add(project.ProjectId, col.ColumnId);

            }

            // Execute the sql, pass in project name and the data table
            var result = ExecuteFunc((con) => con.Query(ProjectSql.CreateProjectAndTasks,
                new
                {
                    ProjectName = project.ProjectName, 
                    Columns = dtCol.AsTableValuedParameter("TVP_Column"),
                    Tasks = dt.AsTableValuedParameter("TVP_Task"),
                    Timelogs = dtTim.AsTableValuedParameter("TVP_Timelogs"),
                    Users = dtUse.AsTableValuedParameter("TVP_User"),
                    //UserTimeLogs = dltUT.AsTableValuedParameter("TVP_UserTimeLog"),
                    //ColumnTasks = dltCT.AsTableValuedParameter("TVP_ColumnTask"),
                    //ProjectUsers = dltPU.AsTableValuedParameter("TVP_ProjectUser"),
                    //TaskTimeLogs = dltTT.AsTableValuedParameter("TVP_TaskTimeLog"),
                    //UserTasks = dltUT.AsTableValuedParameter("TVP_UserTask")
                }));

            var res = ExecuteFunc((con) => con.Query(ProjectSql.CreatLinks,
                new
                {
                    UserTimeLogs = dltUt.AsTableValuedParameter("TVP_UserTimeLog"),
                    ColumnTasks = dltCt.AsTableValuedParameter("TVP_ColumnTask"),
                    ProjectUsers = dltPu.AsTableValuedParameter("TVP_ProjectUser"),
                    TaskTimeLogs = dltTt.AsTableValuedParameter("TVP_TaskTimeLog"),
                    UserTasks = dltUt.AsTableValuedParameter("TVP_UserTask"),
                    ProjectColumns = dltPc.AsTableValuedParameter("TVP_ProjectColumns")
                }));
            // result is the PK of project table
            return 1;
        }
    }
}
