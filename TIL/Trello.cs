using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manatee.Trello;

namespace TIL
{
    public class Trello : ITrello
    {
        private readonly TrelloFactory _client;

        public  Trello() 
        {
            _client = new TrelloFactory();
        }

        //public IMe GetMe()
        //{
        //    return UnwapTask(() => _client.Me());s
        //}
        public async Task<IBoardCollection> GetBoards(string key, string token)
        {
            TrelloAuthorization.Default.AppKey = key;
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
        public async Task<IBoard> GetBoardById(string key, string token, string projectId)
        {
            TrelloAuthorization.Default.AppKey = key;
            TrelloAuthorization.Default.UserToken = token;
            var client = new TrelloFactory();
            var board = client.Board(projectId);
            await board.Refresh();
            return board;
        }
        public async void MoveCard(string key, string token, string cardId, int newPosition)
        {
            TrelloAuthorization.Default.AppKey = key;
            TrelloAuthorization.Default.UserToken = token;
            var client = new TrelloFactory();
            var card = client.Card(cardId);
            card.Position = newPosition;
            await card.Refresh();
        }
    }
    public interface ITrello
    {
        Task<IBoardCollection> GetBoards(string key,string token);
        Task<IBoard> GetBoardById(string key, string token, string projectId);
        void MoveCard(string key, string token, string cardId, int newPosition);
    }
}
