namespace BusStation.Controllers
{
    using BasicWebServer.Server.Controllers;
    using BasicWebServer.Server.HTTP;
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
                return View(model, "/Destinations/All");
            }

            return View();
        }
    }
}
