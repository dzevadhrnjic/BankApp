using BankApp.Model;

namespace BankApp.Services
{
    public interface ILoginService
    {
        Token Login(UserLogin user);
    }
}
