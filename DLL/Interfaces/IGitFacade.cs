using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using Octokit;
using ObjectContracts.DataTransferObjects;

namespace BLL.Interfaces
{
    public interface IGitFacade
    {
     
        IEnumerable<Project> GetProjects(string token);
        User GetUser(string token);

    }
}