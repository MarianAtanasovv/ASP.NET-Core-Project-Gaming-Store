using GameStore.Data;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    using static DataConstants;
   

    public class RegisterUserFormModel
    {
       
        public int UserId { get; set; }

        [Required]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength, ErrorMessage = "Username should be between {2} and {1} characters long.")]
        public string Username { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinlength, ErrorMessage = "Password should be between {2} and {1} characters long.")]
        public string Password { get; init; }

        [Required]
        public string ConfirmPassword { get; init; }


    }
}
