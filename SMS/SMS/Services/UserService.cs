using SMS.Common;
using SMS.Common.Repository;
using SMS.Contracts;
using SMS.Models;
using SMS.Models.Errors;
using SMS.Models.UserViewModels;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SMS.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository data;

        public UserService(IRepository _data)
        {
            data = _data;
        }

        public (bool isValid, ViewError error) ValidateUser(UserRegisterViewModel model)
        {
            var sb = new StringBuilder();
            var isValid = true;

            if (model.Username == null ||
                model.Username.Length < Const.UsernameMinLength ||
                model.Username.Length > Const.UsernameMaxLength)
            {
                isValid = false;
                sb.AppendLine($"Username must be between {Const.UsernameMinLength} and {Const.UsernameMaxLength} characters! ");
            }

            if (string.IsNullOrWhiteSpace(model.Email) ||
                !Regex.IsMatch(model.Email, Const.EmailRegex))
            {
                isValid = false;
                sb.Append("Invalid email address! ");
            }

            if (model.Password == null ||
                model.Password.Length < Const.PasswordMinLength ||
                model.Password.Length > Const.PasswordMaxLength)
            {
                isValid = false;
               sb.Append($"Password must be between {Const.PasswordMinLength} and {Const.PasswordMaxLength} characters! ");
            }

            if (model.Password != model.ConfirmPassword)
            {
                isValid = false;
                sb.AppendLine("Password and ConfirmPasswor are not matching! ");
            }

            var error = new ViewError(sb.ToString().TrimEnd());

            return (isValid, error);
        }

        public void RegisterUser(UserRegisterViewModel model)
        {
            var userExists = GetUserByUsername(model.Username) != null;

            if (userExists)
            {
                throw new ArgumentException("User already exists!");
            }
            
            var user = new User()
            {
                Email = model.Email,
                Username = model.Username,
                Password = HashPassword(model.Password),
                Cart = new Cart()
            };

            data.Add(user);
            data.SaveChanges();
        }

        public (string userId, bool isCorrect) IsLoginCorrect(UserLoginViewModel model)
        {
            var userId = string.Empty;
            var isCorrect = false;

            var user = GetUserByUsername(model.Username);

            if (user != null)
            {
                isCorrect = user.Password == HashPassword(model.Password);
            }

            if (isCorrect)
            {
                userId = user.Id;
            }

            return (userId, isCorrect);
        }

        public string GetUsername(string userId)
        {
            string username = data.All<User>()
                .Where(u => u.Id == userId)
                .Select(u => u.Username)
                .FirstOrDefault();
        
            return username;
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
