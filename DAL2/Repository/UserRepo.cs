using DAL.Interfaces;
using Microsoft.Extensions.Options;

namespace DAL.Repository
{
    public class UserRepo :  IUserRepo
    {
        private string _con;

        public UserRepo(IOptions<ConnectionStrings> connectionStrings) 
        {
            _con = connectionStrings.Value.SqlServer1;
        }

        public bool IsUserAdmin(string token)
        {
            return true;
        }
    }
}

