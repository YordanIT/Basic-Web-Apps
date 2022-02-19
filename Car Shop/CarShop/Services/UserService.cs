using CarShop.Common;
using CarShop.Contracts;
using CarShop.Data.Models;
using CarShop.ViewModels;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CarShop.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository data;
        private readonly IValidationService validationService;

        public UserService(
            IRepository _data,
            IValidationService _validationService
            )
        {
            data = _data;
            validationService = _validationService;
        }

        public string GetUsername(string userId)
        {
            return data.All<User>()
                .FirstOrDefault(u => u.Id == userId)?.Username;
        }

        public string Login(UserLoginViewModel model)
        {
            var user = data.All<User>()
                .Where(u => u.Username == model.Username)
                .Where(u => u.Password == CalculateHash(model.Password))
                .SingleOrDefault();

            return user?.Id;
        }

        public (bool isRegistered, string error) Register(UserRegisterFormModel model)
        {
            bool isRegistered = false;
            string error = null;

            var (isValid, validationError) = validationService.ValidateModel(model);

            if (model.IsMechanic != Const.Mechanic ||
                model.IsMechanic != Const.Client)
            {
                validationError += $"User muset be {Const.Mechanic} or {Const.Client}! "; 
            }

            if (!isValid)
            {
                return (isValid, validationError);
            }

            var user = new User()
            {
                Email = model.Email,
                Password = CalculateHash(model.Password),
                Username = model.Username,
                IsMechanic = model.IsMechanic == Const.Mechanic
            };

            try
            {
                data.Add(user);
                data.SaveChanges();
                isRegistered = true;
            }
            catch (Exception)
            {
                error = "Could not save user in DB";
            }

            return (isRegistered, error);
        }

        private string CalculateHash(string password)
        {
            byte[] passworArray = Encoding.UTF8.GetBytes(password);

            using (SHA256 sha256 = SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(passworArray));
            }
        }
    }
}
