using System;
using System.Collections.Generic;
using System.Text;
using BLL.Interfaces;
using ObjectContracts.DataTransferObjects;
using DAL.Interfaces;
using DAL.Models;
using GIL;

namespace BLL.Facades
{
    public class ProjectFacade : IProjectFacade
    {
        private readonly IProjectRepo _projectRepo;
        private readonly IGit _git;

        public ProjectFacade(IProjectRepo projectRepo, IGit git)
        {
            _projectRepo = projectRepo;
            _git = git;
        }


        public IEnumerable<ProjectDto> GetProject(int projectId)
        {
            return _projectRepo.GetProject(projectId);
        }

        public IEnumerable<TaskDto> GetUserTasks(int userId)
        {
            var tasks = _projectRepo.GetUserTasks(userId);


            return tasks;
        }

        public UserDto CheckLogin(string email, string password)
        {
            return _projectRepo.CheckLogin(email, password);
        }

        public int CreateProject(ProjectDto project)
        {
            return _projectRepo.CreateProject(project);
        }

        public int CreateColumn(string columnName, int projectId, int pointsTotal, int addedPointsTotal)
        {
            throw new NotImplementedException();
        }

        public int CreateTask(string taskName, string comments, int pointsTotal, int addedPointsTotal, DateTime startTime,
            DateTime endTime, DateTime expectedEndTime, string taskDone, string taskDeleted, string taskArchived,
            string extentionReason, string addedReason, int columnId)
        {
            throw new NotImplementedException();
        }

        public int CreateTimeLog(DateTime startTime, DateTime endTime, float totalTime, string billable, string archived, int userId,
            int taskId)
        {
            throw new NotImplementedException();
        }

        public UserDto CreateUser(CreateUser createUserRequest)
        {

            return _projectRepo.CreateUser(createUserRequest);
        }

        public int SetTaskUser(int taskId, int userId)
        {
            return _projectRepo.SetTaskUser(taskId, userId);
        }

        public int SetProjectUser(int projectId, int userId)
        {
            throw new NotImplementedException();
        }

        public int UpdateProject(string projectName, DateTime startTime, DateTime endTime, DateTime expecteDateTime, int pointsTotal,
            int addedPoints, string projectComplete, string projectArchived, int timeIncrement, int projectId)
        {
            throw new NotImplementedException();
        }

        public int UpdateColumn(string columnName, int columnId, int pointsTotal, int addedPointsTotal)
        {
            throw new NotImplementedException();
        }

        public int UpdateTask(string taskName, string comments, int pointsTotal, int addedPointsTotal, DateTime startTime,
            DateTime endTime, DateTime expectedEndTime, string taskDone, string taskDeleted, string taskArchived,
            string extentionReason, string addedReason, int columnId, int taskId)
        {
            throw new NotImplementedException();
        }

        public int UpdateTimeLog(DateTime startTime, DateTime endTime, float totalTime, string billable, string archived,
            int timeLogId)
        {
            throw new NotImplementedException();
        }

        public int UpdateUser(string userName, string role, string email, string password, string accessToken, string archived,
            int UserId)
        {
            throw new NotImplementedException();
        }
    }
}
