using CarShop.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Data.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(Const.UsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsMechanic { get; set; }
    }
}
