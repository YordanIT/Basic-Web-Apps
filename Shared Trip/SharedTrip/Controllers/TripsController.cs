using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SharedTrip.Contracts;
using SharedTrip.Models;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripService service;
        public TripsController(Request request, ITripService _service)
            : base(request)
        {
            service = _service;
        }

        [Authorize]
        public Response Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public Response Add(TripAddFormModel model)
        {
            var(isAdded, errors) = service.AddTrip(model);
            
            if (isAdded)
            {
                return Redirect("/Trips/All");
            }

            return View(errors, "/Error");
        }

        [Authorize]
        public Response All()
        {
            var trips = service.AllTrips();

            return View(trips);
        }

        [Authorize]
        public Response Details(string tripId)
        {
            var trip = service.TripDetails(tripId);

            return View(trip);
        }

        [Authorize]
        public Response AddUserToTrip(string tripId)
        {
            var (isAdded, errors, trip) = service.AddUserToTrip(User.Id, tripId);

            if (!isAdded)
            {
                return View(trip, "/Trips/Details");
            }

            if (!isAdded)
            {
                return View(errors, "/Error");
            }

            return Redirect("/Trips/All");
        }
    }
}
