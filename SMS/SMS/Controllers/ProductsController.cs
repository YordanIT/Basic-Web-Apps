using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SMS.Contracts;
using SMS.Models.Errors;
using SMS.Models.ProductViewModels;
using System;

namespace SMS.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService service;

        public ProductsController(Request request, IProductService _service)
            : base(request)
        {
            service = _service;
        }

        [Authorize]
        public Response Create()
        {
            return View(new { IsAuthenticated = true });
        }

        [Authorize]
        [HttpPost]
        public Response Create(ProductViewModel model)
        {
            var (isValid, error) = service.ValidateProduct(model);

            if (!isValid)
            {
                return View(error, "/Error");
            }

            try
            {
                service.CreateProduct(model);
            }
            catch (ArgumentException ae)
            {
                error = new ViewError(ae.Message);

                return View(error, "/Error");
            }
            catch (Exception)
            {
                error = new ViewError("Unexpected error!");

                return View(error, "/Error");
            }
            
            return Redirect("/");
        }
    }
}
