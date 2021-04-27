using System;
using System.Collections.Generic;
using DAL.Models;
using ProjectDto = ObjectContracts.DataTransferObjects.ProjectDto;
using TaskDto = ObjectContracts.DataTransferObjects.TaskDto;
using UserDto = ObjectContracts.DataTransferObjects.UserDto;

namespace DAL.Interfaces
{
    public interface IProjectRepo
    {
        //IEnumerable<ProjectTaskDto> GetProjects(IEnumerable<int> projectId);
        IEnumerable<ProjectDto> GetProject(int projectId);
        IEnumerable<TaskDto> GetUserTasks(int userId);
        UserDto CheckLogin(string email, string password);
        int CreateProject(CreateProject project);
        int CreateColumn(CreateColumn column);
        int CreateTask(CreateTask task);
        int CreateTimeLog(CreateTimeLog timelog);
        UserDto CreateUser(CreateUser createUser);
        int SetTaskUser(int taskId, int userId);
        int SetProjectUser(int projectId, int userId);

        int UpdateProject(UpdateProject project);
        int UpdateColumn(UpdateColumn column);
        int UpdateTask(UpdateTask task);
        int UpdateTimeLog(UpdateTimeLog timelog);
        int UpdateUser(UpdateUser user);
    }
}