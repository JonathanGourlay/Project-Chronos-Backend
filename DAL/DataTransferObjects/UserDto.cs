namespace DAL.DataTransferObjects
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public UserDto() {}

        public UserDto(UserViewDto dto)

        {
            UserId = dto.UserId;
            UserName = dto.UserName;
            Role = dto.Role;
        }
    }
}