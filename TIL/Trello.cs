using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manatee.Trello;
using Microsoft.Extensions.Options;
using ObjectContracts;
using ObjectContracts.DataTransferObjects;

namespace TIL
{
    public class Trello : ITrello
    {
        private readonly TrelloFactory _client;

        public  Trello(IOptions<AppSettings> options) 
        {
            _client = new TrelloFactory();
            TrelloAuthorization.Default.AppKey = options.Value.TrelloKey;
        }

        //public IMe GetMe()
        //{
        //    return UnwapTask(() => _client.Me());s
        //}
        public async Task<IBoardCollection> GetBoards( string token)
        {
           ;
            TrelloAuthorization.Default.UserToken = token;
            var client = new TrelloFactory();
            var me = await client.Me();
            var boards = me.Boards;
            await me.Refresh();
            await boards.Refresh();
            foreach (var board in boards)
            {
                await board.Cards.Refresh();
                await board.Lists.Refresh();
            }
            return boards;
        }
        public async Task<IBoard> GetBoardById(string token, string projectId)
        {
           
            TrelloAuthorization.Default.UserToken = token;
            var client = new TrelloFactory();
            var board = client.Board(projectId);
            await board.Refresh();
            return board;
        }
        public async void MoveCard( string token, string cardId, string newPosition, string boardId)
        {
            TrelloAuthorization.Default.UserToken = token;
            var client = new TrelloFactory();
            var card = client.Card(cardId);
            var newCol = client.List(newPosition);
            var board = client.Board(boardId);
           
                try
                {
                    card.List = new List(newCol.Id, TrelloAuthorization.Default)
                    {
                        Board = board,
                        IsArchived = newCol.IsArchived,
                        IsSubscribed = newCol.IsSubscribed,
                        Name = newCol.Name,
                        Position = newCol.Position
                    };
                    //card.List = newCol;
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
               
            
          
            await card.Refresh();
            
        }
        public async void DeleteCard(string token, string cardId)
        {
            TrelloAuthorization.Default.UserToken = token;
            var client = new TrelloFactory();
            var card = client.Card(cardId);
          
            
                try
                {
                    card.Delete();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }

            

            await card.Refresh();

        }

        public async void AddCard(string token, string listId, string position, TaskDto task)
        {
            TrelloAuthorization.Default.UserToken = token;
            var client = new TrelloFactory();
            var newCol = client.List(position);
            try
            {
               await client.List(listId).Cards.Add(task.TaskName, task.Comments, newCol.Position, task.ExpectedEndTime,
                    task.TaskDone.Equals("True"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        
    }


}

    public interface ITrello
    {
        Task<IBoardCollection> GetBoards(string token);
        Task<IBoard> GetBoardById(string token, string projectId);
        void MoveCard( string token, string cardId, string newPosition, string boardId);
        void DeleteCard(string token, string cardId);
        void AddCard(string token, string listId, string position, TaskDto task);
    }
}
