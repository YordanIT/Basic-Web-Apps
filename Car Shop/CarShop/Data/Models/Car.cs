using CarShop.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShop.Data.Models
{
    public class Car
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(Const.ModelMaxLength)]
        public string Model { get; set; }

        public int Year { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        [MaxLength(Const.PlateNumberMaxLength)]
        public string PlateNumber { get; set; }

        [Required]
        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<Issue> Issues { get; set; } = new HashSet<Issue>();
    }
}
