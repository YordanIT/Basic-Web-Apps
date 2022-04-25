using BusStation.ViewModels;

namespace BusStation.Contracts
{
    public interface IDestinationService
    {
        bool AddDestination(DestinationFormModel model);

        IEnumerable<DestinationListViewModel> AllDestinations();
    }
}
