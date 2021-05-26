using System.ComponentModel.DataAnnotations;

namespace ObjectContracts.DataTransferObjects
{
    public class UserDto
    {
        [Required]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public string Archived { get; set; }

        public UserDto()
        {
        }

        public UserDto(UserViewDto dto)

        {
            UserId = dto.UserId;
            UserName = dto.UserName;
            Role = dto.Role;
            Email = dto.Role;
            Password = dto.Password;
            AccessToken = dto.AccessToken;
            Archived = dto.Archived;
        }

    }
}