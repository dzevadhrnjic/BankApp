using BankApp.Accounts.Models.DTO;

namespace BankApp.Accounts.Services
{
    public interface IAccountService
    {
       List<AccountDTO> GetAllAccounts(int pageSize, int pageNumber);
       AccountDTO GetAccount(int id);
       List<AccountDTO> GetAccountsByUserId(string token);
       AccountDTO GetAccountByIdAndUserId(int id, string token);
       AccountDTO CreateAccount(string token, AccountDTO accountDto);
       void DeleteAccount(string token, int id);
       AccountDTO UpdateAccount(string token, int id, AccountDTO accountDto);
    }
}
