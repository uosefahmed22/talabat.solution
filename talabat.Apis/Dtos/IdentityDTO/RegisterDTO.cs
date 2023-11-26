using System.ComponentModel.DataAnnotations;

namespace talabat.Apis.Dtos.IdentityDTO
{

    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*\W).{8,}$",
                          ErrorMessage = "Password must contain at least one uppercase letter, " +
                                         "one lowercase letter, one digit, one special character")]
        public string Password { get; set; }
    }
}
