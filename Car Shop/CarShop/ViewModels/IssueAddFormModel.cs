using CarShop.Common;
using System.ComponentModel.DataAnnotations;

namespace CarShop.ViewModels
{
    public class IssueAddFormModel
    {
        [StringLength(Const.DescriptionMinLength,
            ErrorMessage = "{0} must be at least {1} characters!")]
        public string Description { get; set; }
    }
}
