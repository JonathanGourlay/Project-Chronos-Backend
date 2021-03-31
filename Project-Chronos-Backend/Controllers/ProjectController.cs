using System;
using System.Collections.Generic;
using System.Net;
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
        [Route("CreateProject")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult CreateProject([FromBody] IEnumerable<string> projectName)
        {
            return MapToIActionResult(() => _projectRepo.CreateProject(projectName));
        }

        [HttpPost]
        [Route("CreateColumn")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult CreateColumn([FromBody] CreateColumn column)
        {
            return MapToIActionResult(() => _projectRepo.CreateColumn(column.columnName, column.projectId));
        }

        [HttpPost]
        [Route("CreateTask")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult CreateTask([FromBody] CreateTask task)
        {
            return MapToIActionResult(() => _projectRepo.CreateTask(task.taskName, task.comments, task.columnId));
        }

        [HttpPost]
        [Route("CreateTimeLog")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult CreateTimeLog([FromBody] CreateTimeLog timelog)
        {
            return MapToIActionResult(() =>
                _projectRepo.CreateTimeLog(timelog.startTime, timelog.endTime,
                    timelog.totalTime, timelog.userId, timelog.taskId));
        }

        [HttpPost]
        [Route("CreateUser")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult CreateUser([FromBody] CreateUser user)
        {
            return MapToIActionResult(() => _projectRepo.CreateUser(user.userName, user.role));
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
            return MapToIActionResult(() => _projectRepo.UpdateProject(project.projectName, project.projectId));
        }
        [HttpPost]
        [Route("UpdateColumn")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public IActionResult UpdateColumn([FromBody] UpdateColumn column)
        {
            return MapToIActionResult(() => _projectRepo.UpdateColumn(column.columnName, column.columnId));
        }
        [HttpPost]
        [Route("UpdateTask")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public IActionResult UpdateTask([FromBody] UpdateTask task)
        {
            return MapToIActionResult(() => _projectRepo.UpdateTask(task.taskName,task.comments,task.taskId));
        }
        [HttpPost]
        [Route("UpdateTimeLog")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public IActionResult UpdateTimeLog([FromBody]UpdateTimeLog timelog)
        {
           
            return MapToIActionResult(() => _projectRepo.UpdateTimeLog( timelog.startTime, timelog.endTime, timelog.totalTime, timelog.timelogId));
        }
        [HttpPut]
        [Route("UpdateUser")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public IActionResult UpdateUser([FromBody] UpdateUser user)
        {

            return MapToIActionResult(() => _projectRepo.UpdateUser(user.userName, user.role, user.userId));
        }

    }
}