namespace DAL.Models
{
    public class CreateUser
    {
        public string userName { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string accessToken { get; set; }
        public string archived { get; set; }

    }
}
