using CarShop.Common;
using System.ComponentModel.DataAnnotations;

namespace CarShop.ViewModels
{
    public class UserRegisterFormModel
    {
        [StringLength(Const.UsernameMaxLength, MinimumLength = Const.UsernameMinLength,
            ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Email must be valid email")]
        public string Email { get; set; }

        [StringLength(Const.UsernameMaxLength, MinimumLength = Const.UsernameMinLength, 
            ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Password and ConfirmPassword must be equal")]
        public string ConfirmPassword { get; set; }

        public string IsMechanic { get; set; }
    }
}
