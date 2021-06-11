using System;
using System.Collections.Generic;
using DAL.Models;
using ObjectContracts.DataTransferObjects;
using ProjectDto = ObjectContracts.DataTransferObjects.ProjectDto;
using TaskDto = ObjectContracts.DataTransferObjects.TaskDto;
using UserDto = ObjectContracts.DataTransferObjects.UserDto;

namespace DAL.Interfaces
{
    public interface IProjectRepo
    {
        //IEnumerable<ProjectTaskDto> GetProjects(IEnumerable<int> projectId);
        IEnumerable<ProjectDto> GetProject(int projectId);
        IEnumerable<ProjectDto> GetUserProjects(int userId);
        IEnumerable<ProjectDto> GetAdminProjects();
        IEnumerable<TaskDto> GetUserTasks(int userId);
        UserDto CheckLogin(string email, string password);
        int CreateProject(CreateProject project);
        int CreateColumn(CreateColumn column);
        int CreateTask(CreateTask task);
        int CreateTimeLog(CreateTimeLog timelog);
        UserDto CreateUser(CreateUser createUser);
        IEnumerable<UserDto> GetUsers();
        int SetTaskUser(int taskId, int userId);
        int SetProjectUser(int projectId, int userId);

        int UpdateProject(ProjectViewDto project);
        int UpdateColumn(UpdateColumn column);
        int UpdateTask(UpdateTask task);
        int UpdateTimeLog(UpdateTimeLog timelog);
        int UpdateUser(UpdateUser user);
        int SetColumnTask(int columndId, int taskId);
        int MoveCard(int columndId, int taskId);
    }
}