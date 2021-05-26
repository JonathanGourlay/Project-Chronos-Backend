using System.Collections.Generic;
using System.Net;
using BLL.BusinessObjects;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ObjectContracts.DataTransferObjects;

namespace Project_Chronos_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : BaseController
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IProjectFacade _projectFacade;

        public ProjectController(ILogger<ProjectController> logger, IProjectFacade projectFacade)
        {
            _logger = logger;
            _projectFacade = projectFacade;
        }

        [HttpPost]
        [Route("GetRepoProjects")]
        [ProducesResponseType(typeof(List<ProjectObject>), 200)]
        public IActionResult GetProject([FromBody] int projectId)
        {
            return MapToIActionResult(() => _projectFacade.GetProject(projectId));
        }
        [HttpPost]
        [Route("GetUserProjects")]
        [ProducesResponseType(typeof(List<ProjectDto>), 200)]
        public IActionResult GetUserProjects([FromBody] int userId)
        {
            return MapToIActionResult(() => _projectFacade.GetUserProjects(userId));
        }
        [HttpPost]
        [Route("GetUserTasks")]
        [ProducesResponseType(typeof(List<TaskObject>), 200)]
        public IActionResult GetUserTasks([FromBody] int userId)
        {
            return MapToIActionResult(() => _projectFacade.GetUserTasks(userId));
        }
        [HttpPost]
        [Route("CheckLogin")]
        [ProducesResponseType(typeof(UserDto), 200)]
        public IActionResult CheckLogin([FromBody] LoginObject details)
        {
            return MapToIActionResult(() => _projectFacade.CheckLogin(details.Email,details.Password));
        }

        [HttpPost]
        [Route("CreateProject")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult CreateProject([FromBody] CreateProject project)
        {
            return MapToIActionResult(() => _projectFacade.CreateProject(project));
        }

        [HttpPost]
        [Route("CreateColumn")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult CreateColumn([FromBody] CreateColumn column)
        {
            return MapToIActionResult(() => _projectFacade.CreateColumn(column));
        }

        [HttpPost]
        [Route("CreateTask")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult CreateTask([FromBody] CreateTask task)
        {
            return MapToIActionResult(() => _projectFacade.CreateTask(task));
        }

        [HttpPost]
        [Route("CreateTimeLog")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult CreateTimeLog([FromBody] CreateTimeLog timelog)
        {
            return MapToIActionResult(() =>
                _projectFacade.CreateTimeLog(timelog));
        }

        [HttpPost]
        [Route("CreateUser")]
        [ProducesResponseType(typeof(UserDto), (int) HttpStatusCode.OK)]
        public IActionResult CreateUser([FromBody] CreateUser user)
        {
            return MapToIActionResult(() => _projectFacade.CreateUser(user));
        }

        [HttpPost]
        [Route("SetProjectUser")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult SetProjectUser(int projectId, int userId)
        {
            return MapToIActionResult(() => _projectFacade.SetProjectUser(projectId, userId));
        }

        [HttpPost]
        [Route("SetTaskUser")]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        public IActionResult SetTaskUser(int taskId, int userId)
        {
            return MapToIActionResult(() => _projectFacade.SetTaskUser(taskId, userId));
        }
        [HttpPost]
        [Route("UpdateProject")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public IActionResult UpdateProject([FromBody] ProjectViewDto project)
        {
            return MapToIActionResult(() => _projectFacade.UpdateProject(project));
        }
        [HttpPost]
        [Route("UpdateColumn")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public IActionResult UpdateColumn([FromBody] UpdateColumn column)
        {
            return MapToIActionResult(() => _projectFacade.UpdateColumn(column));
        }
        [HttpPost]
        [Route("UpdateTask")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public IActionResult UpdateTask([FromBody] UpdateTask task)
        {
            return MapToIActionResult(() => _projectFacade.UpdateTask(task));
        }
        [HttpPost]
        [Route("SetColumnTask")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public IActionResult SetColumnTask(int columnId, int taskId)
        {
            return MapToIActionResult(() => _projectFacade.SetColumnTask(columnId,taskId));
        }
        [HttpPost]
        [Route("UpdateTimeLog")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public IActionResult UpdateTimeLog([FromBody]UpdateTimeLog timelog)
        {
           
            return MapToIActionResult(() => _projectFacade.UpdateTimeLog(timelog));
        }
        [HttpPut]
        [Route("UpdateUser")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public IActionResult UpdateUser([FromBody] UpdateUser user)
        {

            return MapToIActionResult(() => _projectFacade.UpdateUser(user));
        }

    }
}