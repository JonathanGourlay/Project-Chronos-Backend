using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BLL.BusinessObjects;
using BLL.Interfaces;
using DAL.Interfaces;
using GIL;
using ObjectContracts.DataTransferObjects;
using Octokit;

namespace BLL.Facades
{

    public class GitFacade : IGitFacade
    {
        private readonly IProjectRepo _projectRepo;
        private readonly IGit _git;

        public GitFacade(IProjectRepo projectRepo, IGit git)
        {
            _projectRepo = projectRepo;
            _git = git;
        }

        public IEnumerable<ProjectDto> GetRepoProjects(string token, long repoID)
        {
            var projects = _git.GetProject(token, repoID);
            var gitProjects = new List<ProjectDto>();
            var users = _git.GetRepoUsers(token, repoID); 
            var gitUsers = new List<UserDto>();
            foreach (var project in projects)
            {
                var columns = _git.GetColumns(token, project.Id);
              //  var cards = columns.SelectMany(column => _git.GetCards(token, column.Id));
                var issues = _git.GetIssues(token, repoID);
               
                var gitProjectDTO = new ProjectDto();
                var gitcards = new List<TaskDto>();
                var gitColumns = new List<ColumnDto>();

                foreach (var user in users)
                {
                    gitUsers.Add(new UserDto()
                    {
                        UserName = user.Login,
                        UserId = user.Id,
                        Archived = user.Suspended.ToString(),
                        Email = user.Email
                    });
                }
                //foreach (var issue in issues)
                //{
                //    gitcards.Add(new TaskDto()
                //    {
                //        TaskName = issue.Title,
                //        Comments = JsonSerializer.Serialize(issue),
                //        TaskStartTime = issue.CreatedAt.DateTime,
                //        TaskEndTime = issue.ClosedAt.HasValue ? issue.ClosedAt.Value.DateTime : new DateTime()
                //    });
                //}


                gitProjectDTO.ProjectName = project.Name;
                if (columns != null)
                {
                    for (int i = 0; i < columns.Count(); i++)
                    {
                        var column = columns.ToArray()[i];
                        var cards = _git.GetCards(token, column.Id);
                        foreach (var card in cards)
                        {
                            gitcards.Add(new TaskDto()
                            {
                                TaskName = card.Note,
                                Comments = card.ColumnUrl,
                                StartTime = card.CreatedAt.DateTime
                            });
                        }

                        var columnObject = new ColumnDto()
                        {
                            ColumnId = column.Id, ColumnName = column.Name, PointsTotal = 0, AddedPoints = 0,
                            Tasks = gitcards
                        };
                        gitColumns.Add(columnObject);
                    }
                    gitProjectDTO.Columns =gitColumns ;
                    gitProjectDTO.Users = gitUsers;
                }
                gitProjects.Add(gitProjectDTO);
            }
            

            return gitProjects;
        }

        public IEnumerable<Repository> GetRepositories(string token)
        {
            return _git.GetRepositories(token);
        }
        public User GetUser(string token)
        {
            return _git.GetUser(token);
        }

        public IEnumerable<User> GetRepoUsers(string token, long repoId)
        {
            return _git.GetRepoUsers(token,repoId);
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