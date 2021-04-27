using System.Collections.Generic;
using System.Net;
using BLL.BusinessObjects;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ObjectContracts.DataTransferObjects;
using Octokit;

namespace Project_Chronos_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitController : BaseController
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IGitFacade _gitFacade;

        public GitController(ILogger<ProjectController> logger, IProjectFacade projectFacade, IGitFacade gitFacade)
        {
            _logger = logger;
            _gitFacade = gitFacade;
        }

        [HttpPost]
        [Route("GetProject")]
        [ProducesResponseType(typeof(List<Project>), 200)]
        public IActionResult GetProject([FromBody] string token)
        {
            return MapToIActionResult(() => _gitFacade.GetProjects(token));
        }
        [HttpPost]
        [Route("GetUser")]
        [ProducesResponseType(typeof(User), 200)]
        public IActionResult GetUserTasks([FromBody] string token)
        {
            return MapToIActionResult(() => _gitFacade.GetUser(token));
        }
        

    }
}