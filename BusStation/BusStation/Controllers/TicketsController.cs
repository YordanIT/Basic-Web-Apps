using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using BusStation.Contracts;
using BusStation.ViewModels;

namespace BusStation.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService service;
        public TicketsController(Request request, ITicketService _service)
            : base(request)
        {
            service = _service;
        }

        [Authorize]
        public Response Create(int destinationId)
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public Response Create(TicketFormModel model, int destinationId)
        {
            var isAdded = service.AddTicket(model, destinationId);

            if (isAdded)
            {
                return Redirect("/Destinations/All");
            }

            return View();
        }

        [Authorize]
        public Response Reserve(int destinationId)
        {
            var isBooked = service.BookTrip(User.Id, destinationId);

            if (isBooked)
            {
                return Redirect("/Destinations/All");
            }

            return Redirect("/Destinations/All");
        }

        [Authorize]
        public Response MyTickets()
        {
            var tickets = service.AllTickets(User.Id);
            
            return View(tickets);
        }
    }
}
