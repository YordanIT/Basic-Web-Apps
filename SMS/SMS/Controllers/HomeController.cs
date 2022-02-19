using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SMS.Contracts;

namespace SMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly IProductService productService;

        public HomeController(Request request,
            IUserService _userService,
            IProductService _productService) 
            : base(request)
        {
            userService = _userService;
            productService = _productService;
        }

        public Response Index()
        {
            if (User.IsAuthenticated)
            {
                string username = userService.GetUsername(User.Id);

                var viewUser = new
                {
                    Username = username,
                    IsAuthenticated = true,
                    Products = productService.GetProducts()
                };

                return View(viewUser, "/Home/IndexLoggedIn");
            }

            return View(new { IsAuthenticated = false });
        }
    }
}