using System.ComponentModel.DataAnnotations;

namespace BusStation.Data.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(60)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
