using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace GIL
{
    public class Git : IGit
    {
        private readonly GitHubClient _client;
        public Git()
        {
            _client = new GitHubClient(new ProductHeaderValue("Project-Chronos"));
        }

        public User GetUser(string token)
        {
            _client.Connection.Credentials = new Credentials(token);
            return UnwapTask(() => _client.User.Current());
        }
        public IEnumerable<Repository> GetRepos(string token)
        {
            _client.Connection.Credentials = new Credentials(token);
            return UnwapTask(() => _client.Repository.GetAllForCurrent());
        }
        private T UnwapTask<T>(Func<Task<T>> getData)
        {
            var resultTask = getData.Invoke();
            resultTask.Wait();
            return resultTask.Result;
        }
    }

    public interface IGit
    {
        IEnumerable<Repository> GetRepos(string token);
        User GetUser(string token);
    }
}
