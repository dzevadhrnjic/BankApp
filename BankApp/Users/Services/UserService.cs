using BankApp.Users.Data;
using BankApp.Users.Exceptions;
using BankApp.Users.Models;
using BankApp.Users.Utils;

namespace BankApp.Users.Services
{
    public class UserService : IUserService
    {
        HashUtils hashUtils = new HashUtils();
        EmailService emailService = new EmailService();

        private readonly UserDbContext _userContext;

        public UserService(UserDbContext userContext)
        {
            _userContext = userContext;
        }

        public List<User> GetAllUsers(int pageSize, int pageNumber)
        {
            var result = _userContext.Users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return result;
        }

        public User GetById(int id)
        {
            var user = _userContext.Users.Find(id);

            if (user == null)
            {
                throw new UserNotFoundException("No user with that id");
            }

            return user;
        }

        public User CreateUser(User user)
        {
            //UserValidationService.UserFieldsValidation(user);

            user.CreatedAt = DateTime.Now;
            user.Password = hashUtils.HashPassword(user.Password);
            user.VerifyEmail = false;

            _userContext.Users.Add(user);
            _userContext.SaveChanges();

            emailService.SendEmail(user.Email, "Bank Application ",
                "Welcome to bank " + user.FirstName + ", Code : " + emailService.GetRandomNumbers());

            user.Password = string.Empty;

            return user;
        }

        public void DeleteUser(int id)
        {
            var user = GetById(id);

            if (user == null)
            {
                throw new UserNotFoundException("No user with that id");
            }

            _userContext.Users.Remove(user);
            _userContext.SaveChanges();
        }

        public User UpdateUser(int id, User user)
        {
            var userById = GetById(id);

            if (userById == null)
            {
                throw new UserNotFoundException("Can't find user with that id");
            }

            userById.FirstName = user.FirstName;
            userById.LastName = user.LastName;
            userById.Address = user.Address;
            userById.PhoneNumber = user.PhoneNumber;
            userById.Email = user.Email;
            userById.Password = user.Password;

            _userContext.SaveChanges();

            return user;
        }
    }
}
