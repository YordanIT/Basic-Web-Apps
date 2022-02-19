using SharedTrip.Models;
using System.Collections.Generic;

namespace SharedTrip.Contracts
{
    public interface ITripService
    {
        (bool isValid, IEnumerable<ErrorViewModel> errors) AddTrip(TripAddFormModel model);

        IEnumerable<TripListedViewModel> AllTrips();

        TripViewModel TripDetails(string tripId);

        (bool isAdded, IEnumerable<ErrorViewModel> errors, TripViewModel trip) AddUserToTrip(string userId, string tripId);
    }
}
