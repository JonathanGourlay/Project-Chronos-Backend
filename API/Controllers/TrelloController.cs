using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Manatee.Trello;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ObjectContracts.DataTransferObjects;
using DAL.Models;

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
        public IActionResult GetBoards([FromBody] string token)
        {
           return MapToIActionResult(() => _trelloFacade.GetBoards(token));
         
        }
        [HttpPut]
        [Route("GetBoardById")]
        [ProducesResponseType(typeof(ProjectDto), 200)]
        public IActionResult GetBoardById([FromBody] GetBoardByIdRequest request)
        {
            return MapToIActionResult(() => _trelloFacade.GetBoardById( request.token, request.boardId));

        }
        [HttpPut]
        [Route("MoveCard")]
        [ProducesResponseType(typeof(ProjectDto), 200)]
        public void MoveCard([FromBody] MoveCardRequest request)
        {
            _trelloFacade.MoveCard( request.token, request.cardId, request.newPosition, request.boardId);

        }
        [HttpPut]
        [Route("DeleteCard")]
        [ProducesResponseType(typeof(DeleteCardRequest), 200)]
        public void Delete([FromBody] DeleteCardRequest request)
        {
            _trelloFacade.DeleteCard(request.token, request.cardId);

        }
        [HttpPut]
        [Route("AddCard")]
        [ProducesResponseType(typeof(AddCardRequest), 200)]
        public void AddCard([FromBody] AddCardRequest request)
        {
            _trelloFacade.AddCard(request.token, request.listId, request.position, request.task);

        }

    }

}
