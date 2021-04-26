namespace DLL.Interfaces
{
    public interface IUserRepo
    {
        bool IsUserAdmin(string token);
    }
}
