using BankApp.ChangePassword.Models;
using BankApp.EmailVerification.Data;
using BankApp.EmailVerification.Models.DTO;
using BankApp.EmailVerification.Services;
using BankApp.Users.Data;
using BankApp.Users.Exceptions;
using BankApp.Users.Services;
using BankApp.Users.Utils;

namespace BankApp.ChangePassword.Services
{
    public class ChangePassword : IChangePasswordService
    {
        EmailService emailService = new EmailService();
        VerificationDTO verificationDTO = new VerificationDTO();
        HashUtils hashUtils = new HashUtils();

        private readonly IUserService _userService;
        private readonly VerificationDbContext _verificationDbContext;
        private readonly UserDbContext _userDbContext;
        private readonly TokenUtil _tokenUtil;

        public ChangePassword(IUserService userService, VerificationDbContext verificationDbContext, UserDbContext userDbContext, TokenUtil tokenUtil)
        {
            _userService = userService;
            _verificationDbContext = verificationDbContext;
            _userDbContext = userDbContext;
            _tokenUtil = tokenUtil;
        }

        public void PasswordChange(string token)
        {
            var userId = _tokenUtil.VerifyToken(token);
            var user = _userService.GetById(userId);

            if (user == null)
            {
                throw new ValidationException("Invalid email or password, plese try again");
            }

            string code = emailService.GetRandomNumbers();
            emailService.SendEmail(user.Email, code, "Change your password");

            verificationDTO.Email = user.Email;
            verificationDTO.Code = code;

            _verificationDbContext.Add(verificationDTO);
            _verificationDbContext.SaveChanges();
        }

        public ChangePassword NewPassword(string token, PasswordChange passwordChange)
        {
            var userId = _tokenUtil.VerifyToken(token);
            var user = _userDbContext.Users.FirstOrDefault(x => x.Id == userId);
            var verification = _verificationDbContext.Verifications
                .FirstOrDefault(x => x.Email == user.Email && x.Code == passwordChange.Code);

            if (verification == null)
            {
                throw new InvalidEmailOrPasswordException("Invalid code or password, try again");
            }

            if(!user.Password.Equals(hashUtils.HashPassword(passwordChange.OldPassword)))
            {
                throw new InvalidEmailOrPasswordException("Wrong password, try again");
            }else
            {
                user.Password = hashUtils.HashPassword(passwordChange.NewPassword);
            }

            _userDbContext.Update(user);
            _userDbContext.SaveChanges();

            return null;
        }
    }
}
