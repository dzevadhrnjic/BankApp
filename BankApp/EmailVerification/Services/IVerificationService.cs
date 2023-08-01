using BankApp.EmailVerification.Models;
using BankApp.EmailVerification.Models.DTO;

namespace BankApp.EmailVerification.Services
{
    public interface IVerificationService
    {
        VerificationDTO VerifyEmail(VerificationDTO verificationDTO);
    }
}
