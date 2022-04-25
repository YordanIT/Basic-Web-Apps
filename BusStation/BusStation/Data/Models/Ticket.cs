using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusStation.Data.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Range(10, 90)]
        public decimal Price { get; set; }

        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(Destination))]
        public int? DestinationId { get; set; }
        public Destination? Destination { get; set; }
    }
}
