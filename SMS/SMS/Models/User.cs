using SMS.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Models
{
    public class User
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(Const.UsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(Const.HashedPasswordMaxLength)]
        public string Password { get; set; }

        [Required]
        [ForeignKey(nameof(Cart))]
        public string CartId { get; set; }
        public Cart Cart { get; set; }
    }
}


