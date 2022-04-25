using System.ComponentModel.DataAnnotations;

namespace BusStation.ViewModels
{
    public class DestinationFormModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string DestinationName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Origin { get; set; }

        [Required]
        [MaxLength(30)]
        public string Date { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }
    }
}
