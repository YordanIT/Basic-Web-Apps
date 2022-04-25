using BusStation.Contracts;
using BusStation.Data.Models;
using BusStation.Data.Repository;
using BusStation.ViewModels;
using System.Globalization;

namespace BusStation.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly IRepository repo;
        private readonly IValidationService validation;

        public DestinationService(IRepository _repo, IValidationService _validation)
        {
            repo = _repo;
            validation = _validation;
        }

        public bool AddDestination(DestinationFormModel model)
        {
            DateTime date;
            var isDateValid = DateTime.TryParse
                (model.Date, out date);

            if (!isDateValid)
            {
                return false;
            }

            var isValid = validation.ValidateModel(model);
            
            if (!isValid)
            {
                return false;
            }

            var destination = new Destination
            {
                DestinationName = model.DestinationName,
                Origin = model.Origin,
                ImageUrl = model.ImageUrl,
                Date = date.ToShortDateString(),
                Time = date.ToString("h:mm tt")
            };

            try
            {
                repo.Add(destination);
                repo.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<DestinationListViewModel> AllDestinations()
        {
            var destinations = repo.All<Destination>()
                .Select(d => new DestinationListViewModel
                {
                    Id = d.Id,
                    DestinationName = d.DestinationName,
                    Date = d.Date,
                    Time = d.Time,
                    ImageUrl = d.ImageUrl,
                    Origin = d.Origin,
                    TicketsCount = d.Tickets.Count
                })
                .ToList();

            return destinations;
        }
    }
}
