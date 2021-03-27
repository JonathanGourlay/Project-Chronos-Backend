namespace DAL.Interfaces
{
    public interface IUserRepo
    {
        bool IsUserAdmin(string token);
    }
}
