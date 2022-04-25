using BusStation.ViewModels;

namespace BusStation.Contracts
{
    public interface ITicketService
    {
        bool AddTicket(TicketFormModel model, int id);

        bool BookTrip(string userId, int id);

        IEnumerable<TicketListViewModel> AllTickets(string userId);
    }
}
