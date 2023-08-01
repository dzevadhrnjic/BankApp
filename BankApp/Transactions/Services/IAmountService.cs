using BankApp.Transactions.Models.DTO;

namespace BankApp.Transactions.Services
{
    public interface IAmountService
    {
        Double GetBalanceSource(int accountId);

        Double GetBalanceDestination(int accountId);

        Balance GetBalance(string token, int accountId);
    }
}