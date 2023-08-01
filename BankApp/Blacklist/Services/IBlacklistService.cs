using BankApp.Blacklist.Models.DTO;

namespace BankApp.Blacklist.Services
{
    public interface IBlacklistService
    {
        BlacklistTokenDTO Logout(string token);

        void BlacklistToken(string token);
    }
}
