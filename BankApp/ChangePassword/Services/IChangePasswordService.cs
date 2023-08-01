using BankApp.ChangePassword.Models;

namespace BankApp.ChangePassword.Services
{
    public interface IChangePasswordService
    {
        void PasswordChange(string token);
        ChangePassword NewPassword(string token, PasswordChange passwordChange);
    }
}
