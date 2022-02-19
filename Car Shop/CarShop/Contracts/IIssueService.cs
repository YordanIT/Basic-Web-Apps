using CarShop.ViewModels;
using System.Collections.Generic;

namespace CarShop.Contracts
{
    public interface IIssueService
    {
        (bool isAdded, string error) AddIssue(IssueAddFormModel model, string carId);

        IEnumerable<IssueViewModel> GetAllIssues(string userId);

        void FixIssue(string userId, string issueId);

        void DeleteIssue(string userId, string issueId);

        bool IsUserMechanic(string userId);
    }
}
