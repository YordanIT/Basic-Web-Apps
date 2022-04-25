using BusStation.Contracts;
using System.ComponentModel.DataAnnotations;

namespace BusStation.Services
{
    public class ValidationService : IValidationService
    {
        public bool ValidateModel(object model)
        {
            var context = new ValidationContext(model);
           
            bool isValid = Validator.TryValidateObject(model, context, null, true);

            if (isValid)
            {
                return isValid;
            }

            return isValid;
        }
    }
}
