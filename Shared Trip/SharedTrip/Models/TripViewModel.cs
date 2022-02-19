using System;

namespace SharedTrip.Models
{
    public class TripViewModel
    {
        public string Id { get; init; }
        public string StartPoint { get; init; }

        public string EndPoint { get; init; }

        public string DepartureTime { get; init; }

        public string Seats { get; init; }

        public string Description { get; init; }

    }
}
