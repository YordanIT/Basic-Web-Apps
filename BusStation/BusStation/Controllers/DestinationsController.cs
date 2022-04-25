using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using BusStation.Contracts;
using BusStation.ViewModels;

namespace BusStation.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly IDestinationService service;
        public DestinationsController(Request request, IDestinationService _service)
            : base(request)
        {
            service = _service;
        }

        [Authorize]
        public Response All()
        {
            var destinations = service.AllDestinations();
            
            return View(destinations);
        }

        [Authorize]
        public Response Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public Response Add(DestinationFormModel model)
        {
            var isAdded = service.AddDestination(model);

            if (isAdded)
            {
                return Redirect("/Destinations/All");
            }

            return View();
        }
    }
}
