using System.ComponentModel.DataAnnotations;

namespace Medium.Api.DTO
{
    public class LoginUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
