using System.ComponentModel.DataAnnotations;

namespace BusStation.Data.Models
{
    public class Destination
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string DestinationName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Origin { get; set; }

        [Required]
        [MaxLength(30)]
        public string Date { get; set; }

        [Required]
        [MaxLength(30)]
        public string Time { get; set; }
        
        [Required]
        public string ImageUrl { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
