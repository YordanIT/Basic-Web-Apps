using SharedTrip.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Data.Models
{
    public class Trip
    {

        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(Const.TownNameMaxLength)]
        public string StartPoint { get; set; }

        [Required]
        [MaxLength(Const.TownNameMaxLength)]
        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        [Range(Const.SeatsMinValue, Const.SeatsMaxValue)]
        public int Seats { get; set; }

        [Required]
        [MaxLength(Const.DescriptionMaxLength)]
        public string Description { get; set; }

        [MaxLength(Const.ImagePathMaxLength)]
        public string ImagePath { get; set; }

        public ICollection<UserTrip> UserTrips { get; set; } = new List<UserTrip>();
    }
}
