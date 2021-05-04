using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using ObjectContracts.DataTransferObjects;

namespace BLL.Interfaces
{
    public interface IProjectFacade
    {
        //IEnumerable<ProjectTaskDto> GetRepoProjects(IEnumerable<int> projectId);
        IEnumerable<ProjectDto> GetProject(int projectId);
        IEnumerable<TaskDto> GetUserTasks(int userId);
        UserDto CheckLogin(string email, string password);

        //int Create(ProjectDto project);

        int CreateProject(CreateProject project);
        int CreateColumn(CreateColumn column);
        int CreateTask(CreateTask task);
        int CreateTimeLog(CreateTimeLog timelog);
        UserDto CreateUser(CreateUser createUserRequest);
        int SetTaskUser(int taskId, int userId);
        int SetProjectUser(int projectId, int userId);

        int UpdateProject(UpdateProject project);
        int UpdateColumn(UpdateColumn column);
        int UpdateTask(UpdateTask task);
        int UpdateTimeLog(UpdateTimeLog timelog);
        int UpdateUser(UpdateUser user);
    }
}
