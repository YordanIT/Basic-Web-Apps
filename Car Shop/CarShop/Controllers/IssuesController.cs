using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using CarShop.Contracts;
using CarShop.ViewModels;

namespace CarShop.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssueService issueService;
        public IssuesController(Request request, IIssueService _service)
            : base(request)
        {
            issueService = _service;
        }

        [Authorize]
        [HttpPost]
        public Response Add(IssueAddFormModel model, string carId)
        {
            var (isAdded, error) = issueService.AddIssue(model, carId);

            if (!isAdded)
            {
                return View(new { ErroeMessage = error }, "/Error");
            }

            return View(new { CarId = carId }, "/Issues/CarIssues" );
        }

        [Authorize]
        public Response CarIssues()
        {
            var issues = issueService.GetAllIssues(User.Id);

            return View(issues);
        }

        [Authorize]
        public Response Fix(string issueId)
        {
            var isMechanic = issueService.IsUserMechanic(User.Id);

            if (!isMechanic)
            {
                return View();
            }

            issueService.FixIssue(User.Id, issueId);

            return View();
        }

        [Authorize]
        public Response Delete(string issueId)
        {
            var isMechanic = issueService.IsUserMechanic(User.Id);
            
            if (isMechanic)
            {
                return View();
            }

            issueService.DeleteIssue(User.Id, issueId);

            return View();
        }

    }
}
