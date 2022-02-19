using CarShop.Common;
using CarShop.Contracts;
using CarShop.Data.Models;
using CarShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CarShop.Services
{
    public class CarService : ICarService
    {
        private readonly IValidationService validationService;
        private readonly IRepository data;

        public CarService(IValidationService _validationService,
            IRepository _data)
        {
            validationService = _validationService;
            data = _data;
        }

        public (bool isAdded, string error) AddCar(CarAddFormModel model, string userId)
        {
            Uri uriResult;
            bool isValidUrl = Uri.TryCreate(model.Image, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isValidUrl)
            {
                return (false, "Invalid image!");
            }

            int carYear;
            bool isValidYear = int.TryParse(model.Year, out carYear);

            if (!isValidYear)
            {
                return (false, "Invalid year!");
            }

            bool isValidPlateNumber = Regex.IsMatch(model.PlateNumber, Const.PlateNumberRegex);

            if (!isValidPlateNumber)
            {
                return (false, "Invalid plate number!");
            }

            var (isValid, error) = validationService.ValidateModel(model);

            if (!isValid)
            {
                return (isValid, error);
            }

            var owner = data.All<User>().FirstOrDefault(u => u.Id == userId);

            var car = new Car
            {
                Model = model.Model,
                Year = carYear,
                PictureUrl = uriResult.ToString(),
                PlateNumber = model.PlateNumber,
                Owner = owner
            };

            bool isAdded = false;

            try
            {
                data.Add(car);
                data.SaveChanges();
                isAdded = true;
            }
            catch (Exception)
            {
                error = "Could not add a car!";
            }

            return (isAdded, error);
        }

        public IEnumerable<CarListedViewModel> GetAllCars(string userId)
        {
            var cars = data.All<Car>()
                .Where(c => c.OwnerId == userId)
                .Select(c => new CarListedViewModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    PictureUrl = c.PictureUrl,
                    Year = c.Year.ToString(),
                    PlateNumber = c.PlateNumber
                })
                .ToList();

            return cars;
        }
    }
}
