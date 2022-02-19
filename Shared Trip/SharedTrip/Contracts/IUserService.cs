using SharedTrip.Models;
using System.Collections.Generic;

namespace SharedTrip.Contracts
{
    public interface IUserService
    {
        (bool isRegistered, IEnumerable<ErrorViewModel> errors) Register(UserRegisterFormModel model);

        public (bool isCorrect, string userId) IsLoginCorrect(UserLoginFormModel model);
    }
}
