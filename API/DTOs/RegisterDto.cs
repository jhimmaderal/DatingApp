using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required] // VALIDATION
        public string Username { get; set; }
        [Required] // VALIDATION
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}