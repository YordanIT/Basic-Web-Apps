using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using BusStation.Contracts;
using BusStation.ViewModels;

namespace BusStation.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService service;

        public UsersController(Request request, IUserService _service)
            : base(request)
        {
            service = _service;
        }

        public Response Login()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/Destinations/All");
            }

            return View();
        }

        public Response Register()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/Destinations/All");
            }

            return View();
        }

        [HttpPost]
        public Response Register(UserRegisterFormModel model)
        {
            var isRegistered = service.Register(model);

            if (!isRegistered)
            {
                return Redirect("/Users/Register");
            }

            return Redirect("/Users/Login");
        }

        [HttpPost]
        public Response Login(UserLoginFormModel model)
        {
            var (isCorrect, userId) = service.IsLoginCorrect(model);

            if (!isCorrect)
            {
                return Redirect("/Users/Login");
            }

            SignIn(userId);

            CookieCollection cookies = new CookieCollection();
            cookies.Add(Session.SessionCookieName,
                Request.Session.Id);

            return Redirect("/Destinations/All");
        }

        [Authorize]
        public Response Logout()
        {
            SignOut();

            return Redirect("/");
        }
    }
}
