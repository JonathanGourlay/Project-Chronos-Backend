using System;
using System.Collections.Generic;
using DAL.DataTransferObjects;

namespace DAL.Interfaces
{
    public interface IProjectRepo
    {
        //IEnumerable<ProjectTaskDto> GetProjects(IEnumerable<int> projectId);
        IEnumerable<ProjectDto> GetProject(int projectId);

        //int Create(ProjectDto project);

        int CreateProject(IEnumerable<string> projectName);
        int CreateColumn(string columnName, int projectId);
        int CreateTask(string taskName, string comments, int columnId);
        int CreateTimeLog(DateTime startTime, DateTime endTime, float totalTime, int userId, int taskId);
        int CreateUser(string userName, string role);
        int SetTaskUser(int taskId, int userId);
        int SetProjectUser(int projectId, int userId);

        int UpdateProject(string projectName, int projectId);
        int UpdateColumn(string columnName, int columnId);
        int UpdateTask(string taskName, string comments, int columnId);
        int UpdateTimeLog(DateTime startTime, DateTime endTime, float totalTime, int timeLogId);
        int UpdateUser(string userName, string role, int UserId);
    }
}