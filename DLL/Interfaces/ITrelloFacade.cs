using System.Collections.Generic;
using System.Threading.Tasks;
using Manatee.Trello;
using ObjectContracts.DataTransferObjects;

namespace BLL.Interfaces
{
    public interface ITrelloFacade
    {
       IEnumerable<ProjectDto> GetBoards(string token);
       ProjectDto GetBoardById(string token, string projectId);
       void MoveCard( string token, string cardId, string newPosition, string boardId);
       void DeleteCard(string token, string cardId);
       void AddCard(string token, string listId, string position, TaskDto task);

    }
}