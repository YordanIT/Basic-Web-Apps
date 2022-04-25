using BusStation.Contracts;
using BusStation.Data.Models;
using BusStation.Data.Repository;
using BusStation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.Services
{
    public class UserService : IUserService
    {
        private readonly IValidationService validation;
        private readonly IRepository repo;

        public UserService(IRepository _repo, IValidationService _validation)
        {
            repo = _repo;
            validation = _validation;
        }

        public (bool isCorrect, string userId) IsLoginCorrect(UserLoginFormModel model)
        {
            var isCorrect = false;
            var userId = string.Empty;

            var user = GetUserByUsername(model.Username);

            if (user != null)
            {
                isCorrect = user.Password == HashPassword(model.Password);
            }

            if (isCorrect)
            {
                userId = user.Id;
            }

            return (isCorrect, userId);
        }

        public bool Register(UserRegisterFormModel model)
        {
            var isRegistered = validation.ValidateModel(model);

            if (!isRegistered)
            {
                return false;
            }

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = HashPassword(model.Password)
            };

            try
            {
                repo.Add(user);
                repo.SaveChanges();
                isRegistered = true;
            }
            catch (Exception)
            {
            }

            return isRegistered;
        }

        private User GetUserByUsername(string username)
        {
            return repo.All<User>().FirstOrDefault(u => u.Username == username);
        }

        private string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return string.Empty;
            }
  
            using SHA256 sha256Hash = SHA256.Create();

            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
 
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
