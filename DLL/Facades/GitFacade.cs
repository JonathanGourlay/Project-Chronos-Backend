using System;
using System.Collections.Generic;
using System.Text;
using BLL.Interfaces;
using DAL.Interfaces;
using GIL;
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

        public IEnumerable<Project> GetProjects(string token)
        {
            return _git.GetProjects(token);
        }
        public User GetUser(string token)
        {
            return _git.GetUser(token);
        }

    }
}