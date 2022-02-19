using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SMS.Contracts;

namespace SMS.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICartService service;

        public CartsController(Request request, ICartService _service)
            : base(request)
        {
            service = _service;
        }

        [Authorize]
        public Response AddProduct(string productId)
        {
            var products = service.AddProduct(productId, User.Id);

            return View(new
            {
                Products = products,
                IsAuthenticated = true
            }, "/Carts/Details");
        }

        [Authorize]
        public Response Details()
        {
            var products = service.GetProducts(User.Id);

            return View(products);
        }

        [Authorize]
        public Response Buy()
        {
            service.BuyProducts(User.Id);

            return Redirect("/");
        }
    }
}
