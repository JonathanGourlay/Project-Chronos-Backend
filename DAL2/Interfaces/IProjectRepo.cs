using System;
using System.Collections.Generic;
using Project_Chronos_Backend.DAL.DataTransferObjects;
using Project_Chronos_Backend.Objects;

namespace Project_Chronos_Backend.DAL.Interfaces
{
    public interface IProjectRepo
    {
        IEnumerable<ProjectTaskDto> GetProjects(IEnumerable<int> projectId);
        IEnumerable<ProjectObject> GetProject(int projectId);

        int Create(ProjectObject project);

       int CreateProject(IEnumerable<string> projectName);
       int CreateColumn(IEnumerable<string> columnName, int projectId);
       int CreateTask(string taskName, string comments, int columnId);
       int CreateTimeLog(DateTime startTime, DateTime endTime, float totalTime, int userId, int taskId);
       int CreateUser(string userName, string role);
       int SetTaskUser(int taskId, int userId);
       int SetProjectUser(int projectId, int userId);
    }
}
