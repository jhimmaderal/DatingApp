using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required] // VALIDATION
        public string Username { get; set; }
        [Required] // VALIDATION
        public string Password { get; set; }
    }
}