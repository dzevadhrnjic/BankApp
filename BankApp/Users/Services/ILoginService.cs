using BankApp.Users.Models;

namespace BankApp.Users.Services
{
    public interface ILoginService
    {
        Token Login(UserLogin user);
    }
}
