using System.ComponentModel.DataAnnotations;

namespace BusStation.ViewModels
{
    public class TicketFormModel
    {
        [Range(10, 90)]
        public decimal Price { get; set; }

        [Range(1, 10)]
        public int TicketsCount { get; set; }
    }
}
