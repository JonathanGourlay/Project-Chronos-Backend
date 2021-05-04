using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Manatee.Trello;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ObjectContracts.DataTransferObjects;

namespace Project_Chronos_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrelloControllerrr: BaseController
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly ITrelloFacade _trelloFacade;

        public TrelloControllerrr(ILogger<ProjectController> logger, ITrelloFacade trelloFacade)
        {
            _logger = logger;
            _trelloFacade = trelloFacade;
        }
        [HttpPut]
        [Route("GetBoards")]
        [ProducesResponseType(typeof(List<ProjectDto>), 200)]
        public IActionResult GetBoards([FromBody]string key, string token)
        {
           return MapToIActionResult(() => _trelloFacade.GetBoards(key,token));
         
        }
        [HttpPut]
        [Route("GetBoardById")]
        [ProducesResponseType(typeof(ProjectDto), 200)]
        public IActionResult GetBoardById([FromBody] string key, string token, string projectId)
        {
            return MapToIActionResult(() => _trelloFacade.GetBoardById(key, token, projectId));

        }
        [HttpPut]
        [Route("MoveCard")]
        [ProducesResponseType(typeof(ProjectDto), 200)]
        public void MoveCard([FromBody] string key, string token, string cardId, int newPosition)
        {
            _trelloFacade.MoveCard(key, token, cardId,newPosition);

        }
    }
}
