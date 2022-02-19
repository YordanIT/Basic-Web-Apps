using CarShop.ViewModels;
using System.Collections.Generic;

namespace CarShop.Contracts
{
    public interface ICarService
    {
        (bool isAdded, string error) AddCar(CarAddFormModel model, string userId);

        IEnumerable<CarListedViewModel> GetAllCars(string userId);
    }
}
