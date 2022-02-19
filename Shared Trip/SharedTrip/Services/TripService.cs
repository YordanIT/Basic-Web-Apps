using SharedTrip.Common;
using SharedTrip.Contracts;
using SharedTrip.Data.Models;
using SharedTrip.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SharedTrip.Services
{
    public class TripService : ITripService
    {
        private readonly IRepository data;
        private readonly IValidationService service;

        public TripService(IRepository _data, IValidationService _service)
        {
            data = _data;
            service = _service;
        }

        public (bool isValid, IEnumerable<ErrorViewModel> errors) AddTrip(TripAddFormModel model)
        {
            var (isValid, errors) = service.ValidateModel(model);

            if (!isValid)
            {
                return (false, errors);
            }

            DateTime date;
            var isDateValid = DateTime.TryParseExact
                (model.DepartureTime, "yyyy-MM-dd HH.mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

            if (!isDateValid)
            {
                return (false, new List<ErrorViewModel>
                { new ErrorViewModel("Invalid format of DepartureTime!") });
            }

            var trip = new Trip
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                Description = model.Description,
                DepartureTime = date,
                ImagePath = model.ImagePath,
                Seats = model.Seats
            };

            try
            {
                data.Add(trip);
                data.SaveChanges();
            }
            catch (Exception)
            {
                return (false, new List<ErrorViewModel>
                { new ErrorViewModel("Could not add trip to Db!") });
            }

            isValid = true;

            return (isValid, errors);
        }

        public (bool isAdded, IEnumerable<ErrorViewModel> errors, TripViewModel trip) AddUserToTrip(string userId, string tripId)
        {
            var trip = data.All<Trip>().FirstOrDefault(t => t.Id == tripId);
            var user = data.All<User>().FirstOrDefault(u => u.Id == userId);

            var isAdded = false;
            var errors = new List<ErrorViewModel>();
            var tripView = new TripViewModel();

            if (data.All<UserTrip>()
                .Any(ut => ut.UserId == userId&& ut.TripId == tripId))
            {
                tripView = new TripViewModel
                {
                    Id = trip.Id,
                    StartPoint = trip.StartPoint,
                    EndPoint = trip.EndPoint,
                    DepartureTime = trip.DepartureTime.ToString("yyyy-MM-dd HH.mm"),
                    Description = trip.Description,
                    Seats = trip.Seats.ToString()
                };

                return (isAdded, errors, tripView);
            }
                        
            var userTrip = new UserTrip
            {
                Trip = trip,
                User = user
            };

            try
            {
                data.Add(userTrip);
                trip.Seats--;
                data.SaveChanges();
                isAdded = true;
            }
            catch (Exception)
            {
                errors.Add(new ErrorViewModel("Invalid operation!"));
                return (isAdded, errors, tripView);
            }

            return (isAdded, errors, tripView);
        }

        public IEnumerable<TripListedViewModel> AllTrips()
        {
            var trips = data.All<Trip>()
                .Select(t => new TripListedViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("yyyy-MM-dd HH.mm"),
                    Seats = t.Seats.ToString()
                }).ToList();

            return trips;

        }

        public TripViewModel TripDetails(string tripId)
        {
            var trip = data.All<Trip>()
                .Where(t => t.Id == tripId)
                .Select(t => new TripViewModel
                {
                    Id = tripId,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("yyyy-MM-dd HH.mm"),
                    Seats = t.Seats.ToString(),
                    Description = t.Description
                })
                .FirstOrDefault();

            return trip;
        }
    }
}
