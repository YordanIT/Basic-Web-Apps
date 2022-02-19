using CarShop.Common;
using CarShop.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CarShop.ViewModels
{
    public class CarAddFormModel
    {
        [StringLength(Const.ModelMaxLength, MinimumLength = Const.ModelMinLength,
            ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string Model { get; set; }

        [Required]
        public string Year { get; set; }

        [Required]
        public string Image { get; set; }

        [StringLength(Const.PlateNumberMaxLength,
            ErrorMessage = "{0} must be {1} characters")]
        public string PlateNumber { get; set; }

        public User User { get; set; }
    }
}
