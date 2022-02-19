using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;

namespace SharedTrip.Controllers
{

    public class HomeController : Controller
    {
        public HomeController(Request request)
            : base(request)
        {

        }

        public Response Index()
        {
            if (User.IsAuthenticated)
            {
                var model = new
                {
                    IsAuthenticated = true
                };
                return View(model, "/Trips/All");
            }

            return View();
        }
    }
}