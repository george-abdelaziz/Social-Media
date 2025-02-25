using System.ComponentModel.DataAnnotations;

namespace Model.Dto.ApplicationUserDTO
{
    public class RegisterApplicationUserDto
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
