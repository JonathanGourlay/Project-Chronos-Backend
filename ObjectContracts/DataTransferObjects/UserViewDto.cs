namespace ObjectContracts.DataTransferObjects
{
    public class UserViewDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public string Archived { get; set; }
        public int linkUserTaskId { get; set; }
    }
}