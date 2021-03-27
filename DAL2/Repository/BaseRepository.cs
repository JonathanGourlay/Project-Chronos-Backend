using System;
using System.Data;
using System.Data.SqlClient;
using ProjectChronosBackend.DAL;
using Serilog;

namespace Project_Chronos_Backend.DAL.Repository
{
    public class BaseRepository
    {
        private readonly string _sqlconnectionString;

        protected BaseRepository(ConnectionStrings connectionStrings)
        {
            _sqlconnectionString = connectionStrings.SqlServer1;
        }

        protected T ExecuteFunc<T>(Func<IDbConnection, T> getData)
        {
            try
            {
                using var connection = new SqlConnection(_sqlconnectionString);
                connection.Open();
                return getData(connection);
            }
            catch (SqlException ex)
            {
                var exceptionMsg = $"{GetType().FullName}.ExecuteFunc experienced a SqlException";
                Log.Error(ex, exceptionMsg);
                throw new Exception(exceptionMsg, ex);
            }
            catch (Exception ex)
            {
                var exceptionMsg = $"{GetType().FullName}.ExecuteFunc experienced a {ex.GetType()}";
                Log.Error(ex, exceptionMsg);
                throw new Exception(exceptionMsg, ex);
            }
        }
    }
}

