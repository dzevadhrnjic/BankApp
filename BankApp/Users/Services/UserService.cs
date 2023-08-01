using BankApp.EmailVerification.Data;
using BankApp.EmailVerification.Models.DTO;
using BankApp.Users.Data;
using BankApp.Users.Exceptions;
using BankApp.Users.Models;
using BankApp.Users.Models.DTO;
using BankApp.Users.Utils;
using BankApp.Users.Validations;

namespace BankApp.Users.Services
{
    public class UserService : IUserService
    {
        HashUtils hashUtils = new HashUtils();
        EmailService emailService = new EmailService();
        VerificationDTO verificationDTO = new VerificationDTO();

        private readonly UserDbContext _userContext;
        private readonly VerificationDbContext _verificationDbContext;

        public UserService(UserDbContext userContext, VerificationDbContext verificationDbContext)
        {
            _userContext = userContext;
            _verificationDbContext = verificationDbContext;
        }

        public List<UserDTO> GetAllUsers(int pageSize, int pageNumber)
        {
            var users = _userContext.Users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return users;
        }

        public UserDTO GetById(int id)
        {
            var user = _userContext.Users.Find(id);

            if (user == null)
            {
                throw new UserNotFoundException("No user with that id");
            }

            user.Password = string.Empty;

            return user;
        }

        public UserDTO CreateUser(UserDTO userDto)
        {
            //UserValidationService.UserFieldsValidation(user);

            userDto.CreatedAt = DateTime.Now;
            userDto.Password = hashUtils.HashPassword(userDto.Password);
            userDto.VerifyEmail = false;

            _userContext.Users.Add(userDto);
            _userContext.SaveChanges();

            string code = emailService.GetRandomNumbers();

            emailService.SendEmail(userDto.Email, "Bank Application ",
                "Welcome to bank " + userDto.FirstName + ", Code : " + code);

            verificationDTO.Email = userDto.Email;
            verificationDTO.Code = code;

            _verificationDbContext.Add(verificationDTO);
            _verificationDbContext.SaveChanges();

            userDto.Password = string.Empty;

            return userDto;
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

        public UserDTO UpdateUser(int id, UserDTO userDto)
        {
            var userById = GetById(id);

            if (userById == null)
            {
                throw new UserNotFoundException("Can't find user with that id");
            }

            userById.FirstName = userDto.FirstName;
            userById.LastName = userDto.LastName;
            userById.Address = userDto.Address;
            userById.PhoneNumber = userDto.PhoneNumber;
            userById.Email = userDto.Email;
            userById.Password = hashUtils.HashPassword(userDto.Password);

            _userContext.SaveChanges();

            return userDto;
        }
    }
}
