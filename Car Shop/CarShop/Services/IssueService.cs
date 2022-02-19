using CarShop.Common;
using CarShop.Contracts;
using CarShop.Data.Models;
using CarShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarShop.Services
{
    public class IssueService : IIssueService
    {
        private readonly IRepository data;
        private readonly IValidationService validationService;

        public IssueService(IRepository _data, IValidationService _validation)
        {
            data = _data;
            validationService = _validation;
        }

        public (bool isAdded, string error) AddIssue(IssueAddFormModel model, string carId)
        {
            var isAdded = false;
                               
            var car = data.All<Car>().FirstOrDefault(c => c.Id == carId);

            if (car == null)
            {
                return (false, "Car does not exist!");
            }

            var (isVAlid, error) = validationService.ValidateModel(model);

            if (!isVAlid)
            {
                return (false, error);
            }

            var issue = new Issue
            {
                Description = model.Description,
                IsFixed = false,
                Car = car
            };

            try
            {
                data.Add(issue);
                data.SaveChanges();
                isAdded = true;
            }
            catch (Exception)
            {
                error = "Could not add a issue!";
            }

            return (isAdded, error);
        }

        public void DeleteIssue(string userId, string issueId)
        {
            var car = GetCar(userId);
            var issue = car.Issues
                .FirstOrDefault(i => i.Id == issueId);

            car.Issues.Remove(issue);
        }

        public void FixIssue(string userId, string issueId)
        {
            var car = GetCar(userId);
            var issue = car.Issues
                .FirstOrDefault(i => i.Id == issueId);

            issue.IsFixed = true;
        }

        public IEnumerable<IssueViewModel> GetAllIssues(string userId)
        {
          var issues = GetCar(userId).Issues
                .Select(i => new IssueViewModel
                {
                    Description = i.Description,
                    IsFixed = i.IsFixed
                });

            return issues;
        }

        public bool IsUserMechanic(string userId)
        {
            var isMechanic = data.All<User>()
                 .FirstOrDefault(u => u.Id == userId)
                 .IsMechanic;

            return isMechanic;
        }

        private Car GetCar(string userId)
        {
            var car = data.All<Car>()
                .FirstOrDefault(c => c.OwnerId == userId);
                
            return car;
        }

    }
}
