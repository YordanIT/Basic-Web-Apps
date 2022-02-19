using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using CarShop.Contracts;
using CarShop.ViewModels;

namespace CarShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(Request request, IUserService _userService)
            : base(request)
        {
            userService = _userService;
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
            string id = userService.Login(model);

            if (id == null)
            {
                return View(new { ErrorMessage = "Incorect username or password!" }, "/Error");
            }

            SignIn(id);

            CookieCollection cookies = new CookieCollection();
            cookies.Add(Session.SessionCookieName,
                Request.Session.Id);

            return Redirect("/Cars/All");
        }

        public Response Register()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        public Response Register(UserRegisterFormModel model)
        {
            var (isRegistered, error) = userService.Register(model);

            if (isRegistered)
            {
                return Redirect("/Users/Login");
            }

            return View(new { ErrorMessage = error }, "Error");
        }

        [Authorize]
        public Response Logout()
        {
            SignOut();

            return Redirect("/");
        }
    }
}
