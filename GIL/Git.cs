using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public IEnumerable<Repository> GetRepositories(string token)
        {
            _client.Connection.Credentials = new Credentials(token);
            return UnwapTask(() => _client.Repository.GetAllForCurrent());
        }

        public User GetUser(string token)
        {
            _client.Connection.Credentials = new Credentials(token);
            return UnwapTask(() => _client.User.Current());
        }

        public IEnumerable<User> GetRepoUsers(string token, long repoId)
        {
            _client.Connection.Credentials = new Credentials(token);
            return UnwapTask(() => _client.Repository.Collaborator.GetAll(repoId));
        }
        public IEnumerable<Project> GetProject(string token, long repoId)
        {
            _client.Connection.Credentials = new Credentials(token);
            return UnwapTask(() => _client.Repository.Project.GetAllForRepository(repoId)); 
        }

        public IEnumerable<ProjectColumn> GetColumns(string token, int projectId)
        {
            _client.Connection.Credentials = new Credentials(token);
            return UnwapTask(() => _client.Repository.Project.Column.GetAll(projectId));
        }
        public IEnumerable<ProjectCard> GetCards(string token, int columnId)
        {
            _client.Connection.Credentials = new Credentials(token);
            return UnwapTask(() => _client.Repository.Project.Card.GetAll(columnId));
        }

        public IEnumerable<Issue> GetIssues(string token,long repoId )
        {
            _client.Connection.Credentials = new Credentials(token);
            return UnwapTask(() => _client.Issue.GetAllForRepository(repoId));
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

    public interface IGit
    {
        IEnumerable<ProjectColumn> GetColumns(string token, int projectId);
        IEnumerable<ProjectCard> GetCards(string token, int columnId);
        IEnumerable<Issue> GetIssues(string token, long repoId);
        IEnumerable<Project> GetProject(string token, long repoId);
        IEnumerable<Repository> GetRepositories(string token);
        User GetUser(string token);
        IEnumerable<User> GetRepoUsers(string token, long repoId);
    }
}
