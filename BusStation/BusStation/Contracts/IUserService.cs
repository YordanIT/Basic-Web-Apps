using BusStation.ViewModels;

namespace BusStation.Contracts
{
    public interface IUserService
    {
        bool Register(UserRegisterFormModel model);

        (bool isCorrect, string userId) IsLoginCorrect(UserLoginFormModel model);
    }
}
