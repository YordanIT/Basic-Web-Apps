using SharedTrip.Common;
using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Models
{
    public class TripAddFormModel
    {
        [StringLength(Const.TownNameMaxLength,
            ErrorMessage = "{0} must be less than {1} characters!")]
        public string StartPoint { get; init; }

        [StringLength(Const.TownNameMaxLength,
        ErrorMessage = "{0} must be less than {1} characters!")]
        public string EndPoint { get; init; }

        [Required]
        public string DepartureTime { get; init; }

        [StringLength(Const.ImagePathMaxLength,
            ErrorMessage = "{0} must be less than {1} characters!")]
        public string ImagePath { get; init; }

        [Range(Const.SeatsMinValue, Const.SeatsMaxValue,
            ErrorMessage = "{0} must be between {1} and {2}!")]
        public int Seats { get; init; }

        [StringLength(Const.DescriptionMaxLength,
            ErrorMessage = "{0} must be less than {1} characters!")]
        public string Description { get; set; }
    }
}
