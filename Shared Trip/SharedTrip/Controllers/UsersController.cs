using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SharedTrip.Contracts;
using SharedTrip.Models;
using System.Collections.Generic;

namespace SharedTrip.Controllers
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
                return Redirect("/Trips/All"); 
            }

            return View();
        }

        public Response Register()
        {

            if (User.IsAuthenticated)
            {
                return Redirect("/Trips/All");
            }

            return View();
        }

        [HttpPost]
        public Response Register(UserRegisterFormModel model)
        {
            var (isRegistered, errors) = service.Register(model);

            if (!isRegistered)
            {
                return View(errors, "/Error");
            }

            return Redirect("/Users/Login");
        }

        [HttpPost]
        public Response Login(UserLoginFormModel model)
        {
            var (isCorrect, userId) = service.IsLoginCorrect(model);

            if (!isCorrect)
            {
                var errors = new List<ErrorViewModel> { new ErrorViewModel("Incorrect username or password!") };

                return View(errors, "/Error");
            }

            SignIn(userId);

            CookieCollection cookies = new CookieCollection();
            cookies.Add(Session.SessionCookieName,
                Request.Session.Id);

            return Redirect("/Trips/All");
        }

        [Authorize]
        public Response Logout()
        {
            SignOut();

            return Redirect("/");
        }
    }
}
