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

        public User GetUser(string token)
        {
            _client.Connection.Credentials = new Credentials(token);
            return UnwapTask(() => _client.User.Current());
        }
        public IEnumerable<Project> GetProjects(string token)
        {
            _client.Connection.Credentials = new Credentials(token);
            var user = _client.User.Current().Result;
            var repos = _client.Repository.GetAllForCurrent().Result;
            var projects = new List<Project>();
            foreach (var repo in repos)
            {
               // if (repo.Name == "Project-Chronos-Backend")
               // {
                    var projectList = UnwapTask(() =>
                        _client.Repository.Project.GetAllForRepository(_client.User.Current().Result.Login, repo.Name));
                    
                    if (projectList.Count > 0)
                    {
                        foreach (var project in projectList)
                        {
                            projects.Add(project);
                        } ;
                    }
               // }
            }

            return projects;

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
        IEnumerable<Project> GetProjects(string token);
        User GetUser(string token);
    }
}
