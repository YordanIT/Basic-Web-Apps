using BusStation.Contracts;
using BusStation.Data.Models;
using BusStation.Data.Repository;
using BusStation.ViewModels;

namespace BusStation.Services
{
    public class TicketService : ITicketService
    {
        private readonly IRepository repo;
        private readonly IValidationService validation;

        public TicketService(IRepository _repo, IValidationService _validation)
        {
            repo = _repo;
            validation = _validation;
        }

        public bool AddTicket(TicketFormModel model, int destinationId)
        {
            var isValid = validation.ValidateModel(model);

            if (!isValid)
            {
                return false;
            }

            var destiantion = repo.All<Destination>().First(d => d.Id == destinationId);
            
            var count = model.TicketsCount;

            try
            {
                for (int i = 0; i < count; i++)
                {
                    var ticket = new Ticket
                    {
                        Price = model.Price,
                        Destination = destiantion,
                        DestinationId = destiantion.Id
                    };
                    repo.Add(ticket);
                }
                repo.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool BookTrip(string userId, int destinationId)
        {
            var user = repo.All<User>().First(u => u.Id == userId);
            var destination = repo.All<Destination>().First(d => d.Id == destinationId);
            var ticket = repo.All<Ticket>().FirstOrDefault(t => t.DestinationId == destinationId);
            var isBooked = false;
            
            if (ticket == null)
            {
                return false;
            };

            try
            {
                user.Tickets.Add(ticket);
                repo.SaveChanges();
            }
            catch (Exception)
            {
                return isBooked;
            }
            
            return isBooked;
        }

        public IEnumerable<TicketListViewModel> AllTickets(string userId)
        {
            var tickets = repo.All<Ticket>()
                .Where(t => t.UserId == userId)
                .Select(t => new TicketListViewModel
                {
                    Destination = $"From {t.Destination.DestinationName} to {t.Destination.Origin}",
                    DestinationImage = t.Destination.ImageUrl,
                    DateAndTime = $"Date: {t.Destination.Date}, Hour:{t.Destination.Time}",
                    SingleTicket = t.Price.ToString("0.00")
                })
                .ToList();

            return tickets;
        }
    }
}
