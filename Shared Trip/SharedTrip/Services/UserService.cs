using SharedTrip.Common;
using SharedTrip.Contracts;
using SharedTrip.Data.Models;
using SharedTrip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SharedTrip.Services
{
    public class UserService : IUserService
    {

        private readonly IValidationService validation;
        private readonly IRepository data;

        public UserService(IRepository _data, IValidationService _validation)
        {
            data = _data;
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

        public (bool isRegistered, IEnumerable<ErrorViewModel> errors) Register(UserRegisterFormModel model)
        {
            var (isRegistered, errors) = validation.ValidateModel(model);

            if (!isRegistered)
            {
                return (false, errors);
            }

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = HashPassword(model.Password)
            };

            try
            {
                data.Add(user);
                data.SaveChanges();
                isRegistered = true;
            }
            catch (Exception)
            {
                var error = new ErrorViewModel("Could not save user in DB!");
                errors = new List<ErrorViewModel> { error };
            }

            return (isRegistered, errors);
        }

        private User GetUserByUsername(string username)
        {
            return data.All<User>().FirstOrDefault(u => u.Username == username);
        }

        private string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return string.Empty;
            }

            // Create a SHA256   
            using SHA256 sha256Hash = SHA256.Create();

            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
