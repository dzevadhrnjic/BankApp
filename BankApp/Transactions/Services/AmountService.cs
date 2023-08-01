using BankApp.Accounts.Services;
using BankApp.Transactions.Data;
using BankApp.Transactions.Models.DTO;
using Microsoft.AspNetCore.Http.Metadata;

namespace BankApp.Transactions.Services
{
    public class AmountService : IAmountService
    {
        private readonly TransactionDbContext _transactionDbContext;
        private readonly IAccountService _accountService;

        public AmountService(TransactionDbContext transactionDbContext, IAccountService accountService)
        {
            _transactionDbContext = transactionDbContext;
            _accountService = accountService;
        }

        public Double GetBalanceSource(int accountId)
        {
            var balance = _transactionDbContext.Transactions
                .Where(x => x.SourceAccount == accountId)
                .Select(x => x.Amount).AsEnumerable().Sum();
                

            return balance;
        }

        public Double GetBalanceDestination(int accountId)
        {
            var balance = _transactionDbContext.Transactions
                .Where(x => x.DestinationAccount == accountId)
                .Select(x => x.Amount).AsEnumerable().Sum();

            return balance;
        }

        public Balance GetBalance(string token, int accountId)
        {
            _accountService.GetAccountByIdAndUserId(accountId, token);
           
            Balance balance = new Balance();

            Double sourceAccount = GetBalanceSource(accountId);
            Double destinationAccount = GetBalanceDestination(accountId);

            Double result = sourceAccount - destinationAccount;

            balance.BalanceAccount = result;

            return balance;
        }
    }
}
