using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BLL.Interfaces;
using TIL;
using Manatee.Trello;
using ObjectContracts.DataTransferObjects;
using JsonSerializer = Manatee.Json.Serialization.JsonSerializer;


namespace BLL.Facades
{
    class TrelloFacade : ITrelloFacade
    {
        private readonly ITrello _trello;

        public TrelloFacade(ITrello trello)
        {
            _trello = trello;
        }

        public IEnumerable<ProjectDto> GetBoards(string key, string token)
        {
            var boards = UnwapTask((() => _trello.GetBoards(key, token)));
            //var boards =  _trello.GetBoards(key, token);
            var boardLists = new List<ProjectDto>();
            
            for (int i = 0; i < boards.Count(); i++)
            {
                var board = boards[i];
                boardLists.Add(new ProjectDto()
                {
                    ProjectName = board.Name,
                    Columns = board.Lists.Select((list => new ColumnDto()
                    {
                        ColumnName = list.Name,
                        Tasks = list.Cards.Select((card => new TaskDto() 
                            { TaskName = card.Name,
                                Comments = card.Description,
                                StartTime = card.CreationDate,
                                ExpectedEndTime = card.CreationDate.Date,
                                TaskDone = card.IsComplete.ToString(),
                                TaskArchived = card.IsArchived.ToString(),
                                //Users = card.Members.Count() < 0? card.Members.Select((member => new UserDto(){UserName = member.UserName, Archived = member!.IsConfirmed.ToString()})) : new List<UserDto>()
                            }))


                    } )),
                    AddedPoints = 0,
                    ExpectedEndTime = board.CreationDate,
                    PointsTotal = 0,
                    ProjectStartTime = board.CreationDate,
                    ProjectArchived = board.IsClosed.ToString(),
                    ProjectComplete = board.IsClosed.ToString()
                });
            }
            return boardLists;
        }

        public ProjectDto GetBoardById(string key, string token, string projectId)
        {
            var board = UnwapTask((() => _trello.GetBoardById(key, token, projectId)));
            var boardDto = new ProjectDto();
            var lists = board.Lists;
            boardDto.ProjectName = board.Name;
            boardDto.ExpectedEndTime = board.CreationDate;
            boardDto.ProjectStartTime = board.CreationDate;
            boardDto.ProjectArchived = board.IsClosed.ToString();
            boardDto.ProjectComplete = board.IsClosed.ToString();
            boardDto.Columns = board.Lists.Select((list => new ColumnDto()
                    {
                        ColumnName = list.Name,
                        Tasks = list.Cards.Select((card => new TaskDto()
                        {
                            TaskName = card.Name,
                            Comments = card.Description,
                            StartTime = card.CreationDate,
                            ExpectedEndTime = card.CreationDate.Date,
                            TaskDone = card.IsComplete.ToString(),
                            TaskArchived = card.IsArchived.ToString(),
                        }))
                    }
                ));
            return boardDto;
        }
        public void MoveCard(string key, string token, string cardId, int newPosition)
        {
           _trello.MoveCard(key, token, cardId, newPosition);
        }
        public T UnwapTask<T>(Func<Task<T>> getData)
        {
            var resultTask = getData.Invoke();

            resultTask.Wait();

            if (resultTask.IsFaulted && resultTask.Exception != null)
            {
                throw resultTask.Exception;
            }

            return resultTask.Result;
        }
    }
}
