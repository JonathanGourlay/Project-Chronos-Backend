using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Security.Cryptography;
using DAL.DataTransferObjects;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project_Chronos_Backend.BusinessObjects;
using Project_Chronos_Backend.Models;

namespace Project_Chronos_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : BaseController
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IProjectRepo _projectRepo;

        public ProjectController(ILogger<ProjectController> logger, IProjectRepo projectRepo)
        {
            _logger = logger;
            _projectRepo = projectRepo;
        }

        [HttpPost]
        [Route("GetProject")]
        [ProducesResponseType(typeof(List<ProjectObject>), 200)]
        public IActionResult GetProject([FromBody] int projectId)
        {
            return MapToIActionResult(() => _projectRepo.GetProject(projectId));
        }
        [HttpPost]
        [Route("GetUserTasks")]
        [ProducesResponseType(typeof(List<TaskObject>), 200)]
        public IActionResult GetUserTasks([FromBody] int userId)
        {
            return MapToIActionResult(() => _projectRepo.GetUserTasks(userId));
        }
        [HttpPost]
        [Route("CheckLogin")]
        [ProducesResponseType(typeof(UserDto), 200)]
        public IActionResult CheckLogin([FromBody] LoginObject details)
        {
            return MapToIActionResult(() => _projectRepo.CheckLogin(details.Email,details.Password));
        }

        [HttpPost]
        [Route("CreateProject")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult CreateProject([FromBody] ProjectDto project)
        {
            return MapToIActionResult(() => _projectRepo.CreateProject(project));
        }

        [HttpPost]
        [Route("CreateColumn")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult CreateColumn([FromBody] CreateColumn column)
        {
            return MapToIActionResult(() => _projectRepo.CreateColumn(column.columnName,column.projectId,column.pointsTotal, column.addedPointsTotal));
        }

        [HttpPost]
        [Route("CreateTask")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult CreateTask([FromBody] CreateTask task)
        {
            return MapToIActionResult(() => _projectRepo.CreateTask(task.taskName, task.comments,task.PointsTotal, task.AddedPointsTotal, task.StartTime,task.EndTime,task.ExpectedEndTime,task.TaskDone,task.TaskDeleted,task.TaskArchived,task.ExtensionReason,task.AddedReason, task.columnId));
        }

        [HttpPost]
        [Route("CreateTimeLog")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult CreateTimeLog([FromBody] CreateTimeLog timelog)
        {
            return MapToIActionResult(() =>
                _projectRepo.CreateTimeLog(timelog.startTime, timelog.endTime,
                    timelog.totalTime, timelog.billable,timelog.archived, timelog.userId, timelog.taskId));
        }

        [HttpPost]
        [Route("CreateUser")]
        [ProducesResponseType(typeof(UserDto), (int) HttpStatusCode.OK)]
        public IActionResult CreateUser([FromBody] CreateUser user)
        {
            return MapToIActionResult(() => _projectRepo.CreateUser(user.userName, user.role, user.email,user.password,user.accessToken,user.archived));
        }

        [HttpPost]
        [Route("SetProjectUser")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult SetProjectUser(int projectId, int userId)
        {
            return MapToIActionResult(() => _projectRepo.SetProjectUser(projectId, userId));
        }

        [HttpPost]
        [Route("SetTaskUser")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult SetTaskUser(int taskId, int userId)
        {
            return MapToIActionResult(() => _projectRepo.SetTaskUser(taskId, userId));
        }
        [HttpPost]
        [Route("UpdateProject")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public IActionResult UpdateProject([FromBody] UpdateProject project)
        {
            return MapToIActionResult(() => _projectRepo.UpdateProject(project.projectName,project.ProjectStartTime, project.ProjectEndTime,project.ExpectedEndTime,project.PointsTotal,project.AddedPoints,project.ProjectComplete,project.ProjectArchived,project.TimeIncrement, project.projectId));
        }
        [HttpPost]
        [Route("UpdateColumn")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public IActionResult UpdateColumn([FromBody] UpdateColumn column)
        {
            return MapToIActionResult(() => _projectRepo.UpdateColumn(column.columnName, column.columnId, column.pointsTotal, column.addedPointsTotal));
        }
        [HttpPost]
        [Route("UpdateTask")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public IActionResult UpdateTask([FromBody] UpdateTask task)
        {
            return MapToIActionResult(() => _projectRepo.UpdateTask(task.taskName,task.comments,task.PointsTotal,task.AddedPoints,task.StartTime,task.EndTime,task.ExpectedEndTime,task.TaskDone,task.TaskDeleted,task.TaskArchived,task.ExtensionReason,task.AddedReason,task.columnId,task.taskId));
        }
        [HttpPost]
        [Route("UpdateTimeLog")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public IActionResult UpdateTimeLog([FromBody]UpdateTimeLog timelog)
        {
           
            return MapToIActionResult(() => _projectRepo.UpdateTimeLog( timelog.startTime, timelog.endTime, timelog.totalTime,timelog.billable,timelog.archived, timelog.timelogId));
        }
        [HttpPut]
        [Route("UpdateUser")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public IActionResult UpdateUser([FromBody] UpdateUser user)
        {

            return MapToIActionResult(() => _projectRepo.UpdateUser(user.userName, user.role,user.email,user.password,user.accessToken,user.archived, user.userId));
        }

    }
}