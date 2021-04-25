﻿using System;
using System.Collections.Generic;
using DAL.DataTransferObjects;

namespace DAL.Interfaces
{
    public interface IProjectRepo
    {
        //IEnumerable<ProjectTaskDto> GetProjects(IEnumerable<int> projectId);
        IEnumerable<ProjectDto> GetProject(int projectId);
        IEnumerable<TaskDto> GetUserTasks(int userId);
        UserDto CheckLogin(string email, string password);

        //int Create(ProjectDto project);

        int CreateProject(ProjectDto project);
        int CreateColumn(string columnName, int projectId, int pointsTotal, int addedPointsTotal);
        int CreateTask(string taskName, string comments, int pointsTotal, int addedPointsTotal, DateTime startTime, DateTime endTime, DateTime expectedEndTime, string taskDone, string taskDeleted, string taskArchived, string extentionReason, string addedReason, int columnId);
        int CreateTimeLog(DateTime startTime, DateTime endTime, float totalTime, string billable,string archived, int userId, int taskId);
        int CreateUser(string userName, string role, string email,string password,string accessToken, string archived);
        int SetTaskUser(int taskId, int userId);
        int SetProjectUser(int projectId, int userId);

        int UpdateProject(string projectName, DateTime startTime, DateTime endTime, DateTime expecteDateTime, int pointsTotal, int addedPoints, string projectComplete, string projectArchived, int timeIncrement, int projectId);
        int UpdateColumn(string columnName, int columnId, int pointsTotal, int addedPointsTotal);
        int UpdateTask(string taskName, string comments, int pointsTotal, int addedPointsTotal, DateTime startTime, DateTime endTime, DateTime expectedEndTime, string taskDone, string taskDeleted, string taskArchived, string extentionReason, string addedReason, int columnId, int taskId);
        int UpdateTimeLog(DateTime startTime, DateTime endTime, float totalTime, string billable,string archived, int timeLogId);
        int UpdateUser(string userName, string role,string email, string password, string accessToken, string archived, int UserId);
    }
}