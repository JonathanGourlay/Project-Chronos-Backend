using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DAL.DataTransferObjects;
using GIL.DataTransferObjects;
using GIL.Interfaces;
using DAL.SQL;
using Dapper;
using Microsoft.Extensions.Options;

namespace DAL.Repository
{
    public class GitRepo : BaseRepository, IProjectRepo
    {

        public GitRepo(IOptions<ConnectionStrings> connectionStrings)
            : base(connectionStrings.Value)
        {
           //_con = connectionStrings.Value.SQLServer;
        }

        
    }
}