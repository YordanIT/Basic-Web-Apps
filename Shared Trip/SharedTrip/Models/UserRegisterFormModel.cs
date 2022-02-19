using SharedTrip.Common;
using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Models
{
    public class UserRegisterFormModel
    {
        [StringLength(Const.UsernameMaxLength, MinimumLength = Const.UsernameMinLength,
            ErrorMessage = "{0} must be between {2} and {1} characters!")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Is not valid email address!")]
        public string Email { get; set; }

        [StringLength(Const.PasswordMaxLength, MinimumLength = Const.PasswordMinLength,
            ErrorMessage = "{0} must be between {2} and {1} characters!")]
        public string Password { get; set; }

        [Compare(nameof(Password),
            ErrorMessage = "Password and ConfirmPassword are not matching!")]
        public string ConfirmPassword { get; set; }
    }
}
