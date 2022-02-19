using CarShop.ViewModels;

namespace CarShop.Contracts
{
    public interface IUserService
    {
        (bool isRegistered, string error) Register(UserRegisterFormModel model);

        string Login(UserLoginViewModel model);

        string GetUsername(string userId);
    }
}
