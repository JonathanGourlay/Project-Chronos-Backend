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
        [Route("GetProjects")]
        [ProducesResponseType(typeof(List<ProjectObject>), 200)]
        public IActionResult GetProjects([FromBody] List<int> projectIds)
        {
            return MapToIActionResult(() => _projectRepo.GetProjects(projectIds));
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
        public IActionResult CreateColumn([FromBody] IEnumerable<string> columnName, int projectId)
        {
            return MapToIActionResult(() => _projectRepo.CreateColumn(columnName, projectId));
        }

        [HttpPost]
        [Route("CreateTask")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult CreateTask([FromBody] TaskObject task, int columnId)
        {
            return MapToIActionResult(() => _projectRepo.CreateTask(task.TaskName, task.Comments, columnId));
        }

        [HttpPost]
        [Route("CreateTimeLog")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult CreateTimeLog([FromBody] CreateTimeLog timelog)
        {
            var dto = timelog.timeLog.ToDto();

            return MapToIActionResult(() =>
                _projectRepo.CreateTimeLog(timelog.timeLog.StartTime, timelog.timeLog.EndTime,
                    timelog.timeLog.TotalTime, timelog.userId, timelog.taskId));
        }

        [HttpPost]
        [Route("CreateUser")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult CreateUser(string userName, string role)
        {
            return MapToIActionResult(() => _projectRepo.CreateUser(userName, role));
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
    }
}