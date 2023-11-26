using System.ComponentModel.DataAnnotations;

namespace talabat.Apis.Dtos.IdentityDTO
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } // Ziadbahaa@gmail.com
        [Required]
        public string Password { get; set; }
    }
}
