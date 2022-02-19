using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SMS.Contracts;
using SMS.Models.Errors;
using SMS.Models.UserViewModels;
using System;

namespace SMS.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService service;
        public UsersController(Request request, IUserService _service)
            : base(request)
        {
            service = _service;
        }

        public Response Register()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View(new { IsAuthenticated = false });
        }

        [HttpPost]
        public Response Register(UserRegisterViewModel model)
        {
            var (isValid, error) = service.ValidateUser(model);

            if (!isValid)
            {
                return View(error, "/Error");
            }

            try
            {
                service.RegisterUser(model);
            }
            catch (ArgumentException ae)
            {
                error = new ViewError(ae.Message);

                return View(error, "Error");
            }
            catch (Exception)
            {
                error = new ViewError("Unexpected error!");

                return View(error, "Error");
            }

            return Redirect("/Users/Login");
        }

        public Response Login()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View(new { IsAuthenticated = false });
        }

        [HttpPost]
        public Response Login(UserLoginViewModel model)
        {
            Request.Session.Clear();

            var (userId, isCorrect) = service.IsLoginCorrect(model);

            if (isCorrect)
            {
                SignIn(userId);

                var cookies = new CookieCollection();
                cookies.Add(Session.SessionCookieName,
                    Request.Session.Id);

                return Redirect("/");
            }

            var error = new ViewError("Incorrect username or password!");

            return View(error, "/Error");
        }

        [Authorize]
        public Response Logout()
        {
            SignOut();

            return Redirect("/");
        }
    }
}
