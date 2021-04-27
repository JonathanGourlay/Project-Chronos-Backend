﻿using System;
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

        public int CreateProject(CreateProject project)
        {
            return _projectRepo.CreateProject(project);
        }

        public int CreateColumn(CreateColumn column)
        {
            return _projectRepo.CreateColumn(column);
        }

        public int CreateTask(CreateTask task)
        {
            return _projectRepo.CreateTask(task);
        }

        public int CreateTimeLog(CreateTimeLog timelog)
        {
            return _projectRepo.CreateTimeLog(timelog);
        }

        public UserDto CreateUser(CreateUser user)
        {

            return _projectRepo.CreateUser(user);
        }

        public int SetTaskUser(int taskId, int userId)
        {
            return _projectRepo.SetTaskUser(taskId, userId);
        }

        public int SetProjectUser(int projectId, int userId)
        {
            return _projectRepo.SetProjectUser(projectId, userId);
        }

        public int UpdateProject(UpdateProject project)
        {

            return _projectRepo.UpdateProject(project);
        }

        public int UpdateColumn(UpdateColumn column)
        {
            return _projectRepo.UpdateColumn(column);
        }

        public int UpdateTask(UpdateTask task)
        {
            return _projectRepo.UpdateTask(task);
        }

        public int UpdateTimeLog(UpdateTimeLog timelog)
        {
            return _projectRepo.UpdateTimeLog(timelog);
        }

        public int UpdateUser(UpdateUser user)
        {
            return _projectRepo.UpdateUser(user);
        }
    }
}
