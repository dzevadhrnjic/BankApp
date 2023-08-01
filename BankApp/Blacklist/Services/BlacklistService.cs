
using BankApp.Blacklist.Data;
using BankApp.Blacklist.Exceptions;
using BankApp.Blacklist.Models;
using BankApp.Blacklist.Models.DTO;

namespace BankApp.Blacklist.Services
{
    public class BlacklistService : IBlacklistService
    {
        private readonly BlacklistDbContext _blacklistDbContext;
        public BlacklistService(BlacklistDbContext blacklistDbContext)
        {
            _blacklistDbContext = blacklistDbContext;
        }

        public BlacklistTokenDTO Logout(string token)
        {
            BlacklistTokenDTO blacklistToken = new BlacklistTokenDTO();

            blacklistToken.Token = token;

            _blacklistDbContext.Add(blacklistToken);
            _blacklistDbContext.SaveChanges();

            return blacklistToken;
        }

        public void BlacklistToken(string token)
        {
            var checkBlacklistToken = _blacklistDbContext.BlacklistTokens.FirstOrDefault(x => x.Token == token);

            if (checkBlacklistToken != null)
            {
                throw new BlacklistTokenException("Unauthorized user");
            }
        }
    }
}
