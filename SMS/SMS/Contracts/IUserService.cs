using SMS.Models.Errors;
using SMS.Models.UserViewModels;

namespace SMS.Contracts
{
    public interface IUserService
    {
        (bool isValid, ViewError error) ValidateUser(UserRegisterViewModel model);

        void RegisterUser(UserRegisterViewModel model);

        (string userId, bool isCorrect) IsLoginCorrect(UserLoginViewModel model);

        string GetUsername(string userId);
    }
}
