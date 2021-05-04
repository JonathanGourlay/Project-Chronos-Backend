using System;
using System.Collections.Generic;
using System.Text;
using BLL.BusinessObjects;
using DAL.Models;
using Octokit;
using ObjectContracts.DataTransferObjects;

namespace BLL.Interfaces
{
    public interface IGitFacade
    {

        IEnumerable<Repository> GetRepositories(string token);
        IEnumerable<ProjectDto> GetRepoProjects(string token, long repoId);
        User GetUser(string token);
        IEnumerable<User> GetRepoUsers(string token, long repoId);
    }
}