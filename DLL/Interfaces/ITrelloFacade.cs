using System.Collections.Generic;
using System.Threading.Tasks;
using Manatee.Trello;
using ObjectContracts.DataTransferObjects;

namespace BLL.Interfaces
{
    public interface ITrelloFacade
    {
       IEnumerable<ProjectDto> GetBoards(string key,string token);
       ProjectDto GetBoardById(string key, string token, string projectId);
       void MoveCard(string key, string token, string cardId, int newPosition);

    }
}