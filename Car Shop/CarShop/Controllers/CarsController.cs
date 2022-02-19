using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using CarShop.Contracts;
using CarShop.ViewModels;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService carService;
        public CarsController(Request request, ICarService _carService)
            : base(request)
        {
            carService = _carService;
        }

        [Authorize]
        public Response Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public Response Add(CarAddFormModel model)
        {
            var userId = User.Id;
            var (isAdded, error) = carService.AddCar(model, userId);

            if (isAdded)
            {
                return Redirect("/Cars/All");
            }

            return View(error, "/Error");
        }

        [Authorize]
        public Response All()
        {
            var userId = User.Id;
            var cars = carService.GetAllCars(userId);

            return View(cars);
        }
    }
}
