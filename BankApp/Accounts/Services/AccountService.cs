using BankApp.Accounts.Data;
using BankApp.Accounts.Exceptions;
using BankApp.Accounts.Models;
using BankApp.Accounts.Models.DTO;
using BankApp.Users.Utils;

namespace BankApp.Accounts.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountDbContext _accountDbContext;
        private readonly TokenUtil _tokenUtil;

        public AccountService(AccountDbContext accountDbContext, TokenUtil tokenUtil)
        {
            _accountDbContext = accountDbContext;
            _tokenUtil = tokenUtil;
        }  
        
        public List<AccountDTO> GetAllAccounts(int pageSize, int pageNumber)
        {
            var accounts = _accountDbContext.Accounts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return accounts;
        }

        public AccountDTO GetAccount(int id)
        {
            var account = _accountDbContext.Accounts.FirstOrDefault(x => x.Id == id);

            if (account == null)
            {
                throw new AccountNotFound("No account with that Id");
            }

            return account;
        }

        public List<AccountDTO> GetAccountsByUserId(string token)
        {
            var idUser = _tokenUtil.VerifyToken(token);

            var account = _accountDbContext.Accounts.
                Where(x => x.UserId == idUser)
                .ToList();

            if (account == null)
            {
                throw new AccountNotFound("No account with that userId");
            }

            return account;

        }

        public AccountDTO GetAccountByIdAndUserId(int id, string token)
        {
            var idUser = _tokenUtil.VerifyToken(token);

            var account = _accountDbContext.Accounts.FirstOrDefault(x => x.Id == id && x.UserId == idUser);
            
            if (account == null) 
            {
                throw new AccountNotFound("No account with that id and user id");
            }

            return account;
        }

        public AccountDTO CreateAccount(string token, AccountDTO accountDto)
        {
            int userId = (int)_tokenUtil.VerifyToken(token);

            accountDto.UserId = userId;
            accountDto.CreatedAt = DateTime.Now;
           
            _accountDbContext.Accounts.Add(accountDto);
            _accountDbContext.SaveChanges();

            return accountDto;
        }

        public void DeleteAccount(string token, int id)
        {
            _tokenUtil.VerifyToken(token);
            var user = GetAccountByIdAndUserId(id, token);

            _accountDbContext.Accounts.Remove(user);
            _accountDbContext.SaveChanges();
        }

        public AccountDTO UpdateAccount(string token, int id, AccountDTO accountDto)
        {
            var userId = _tokenUtil.VerifyToken(token);
            var user = GetAccountByIdAndUserId(id, token);
 
            user.Name = accountDto.Name;
            user.IntialBalance = accountDto.IntialBalance;
            user.UserId = userId;
            user.CreatedAt = user.CreatedAt;

            _accountDbContext.SaveChanges();

            return accountDto;
        }
    }
}
