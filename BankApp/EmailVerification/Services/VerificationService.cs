using BankApp.EmailVerification.Data;
using BankApp.EmailVerification.Exceptions;
using BankApp.EmailVerification.Models.DTO;
using BankApp.Users.Data;

namespace BankApp.EmailVerification.Services
{
    public class VerificationService : IVerificationService
    {
        private readonly VerificationDbContext _verificationDbContext;
        private readonly UserDbContext _userDbContext;

        public VerificationService(VerificationDbContext verificationDbContext, UserDbContext userDbContext)
        {
            _verificationDbContext = verificationDbContext;
            _userDbContext = userDbContext;
        }

        public VerificationDTO VerifyEmail(VerificationDTO verificationDTO)
        {
            var verificationMail = _verificationDbContext.Verifications
                .FirstOrDefault(x => x.Email == verificationDTO.Email && x.Code == verificationDTO.Code);
            var userByMail = _userDbContext.Users.FirstOrDefault(x => x.Email == verificationDTO.Email);
            
            if (verificationMail == null)
            {
                throw new EmailVerificationException("Couldn't verify, wrong email or code");
            }

            if (userByMail == null)
            {
                throw new Exception("Can't verify, check email or code");
            }

            verificationMail.Email = verificationMail.Email;
            verificationMail.Code = verificationMail.Code;

            userByMail.VerifyEmail = true;
            
            _userDbContext.SaveChanges();

            return verificationMail;
        }
    }
}
