using BankApp.Accounts.Services;
using BankApp.Transactions.Data;
using BankApp.Transactions.Exceptions;
using BankApp.Transactions.Models.DTO;
using BankApp.Transactions.Validations;
using BankApp.Users.Exceptions;
using BankApp.Users.Models;
using BankApp.Users.Utils;

namespace BankApp.Transactions.Services
{
    public class TransactionService: ITransactionService
    {
        private readonly TransactionDbContext _transactionDbContext;
        private readonly IAccountService _accountService;
        private readonly ITransactionValidationService _transactionValidationService;
        private readonly TokenUtil _tokenUtil;

        public TransactionService(TransactionDbContext transactionDbContext,
            IAccountService accountService, 
            ITransactionValidationService transactionValidationService,
            TokenUtil tokenUtil)
        {
            _transactionDbContext = transactionDbContext;
            _accountService = accountService;
            _transactionValidationService = transactionValidationService;
            _tokenUtil = tokenUtil;
        }

        public List<TransactionDTO> GetAllTransactions(int pageSize, int pageNumber)
        {
            var transactions = _transactionDbContext.Transactions.
                Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return transactions;
        }

        public TransactionDTO GetTransactionById(int id)
        {
            var transaction = _transactionDbContext.Transactions.FirstOrDefault(x => x.Id == id);

            if (transaction == null)
            {
                throw new TransactionNotFound("No transaction with that id");
            }

            return transaction;
        }

        public List<TransactionDTO> GetTransactions(string order, DateTime? dateFrom, DateTime? dateTo)
        {
           
            if (order == "asc" && dateFrom != null && dateTo != null)
            {
                var transactionsAscAndDate = _transactionDbContext.Transactions
                    .Where(x => x.CreatedAt >= dateFrom && x.CreatedAt <= dateTo)
                    .OrderBy(x => x.CreatedAt)
                    .ToList();

                return transactionsAscAndDate;
            }else if (order == "desc" && dateFrom != null && dateTo != null)
            {
                var tansactionsDescAndDate = _transactionDbContext.Transactions
                    .Where(x => x.CreatedAt >= dateFrom && x.CreatedAt <= dateTo)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToList();

                return tansactionsDescAndDate;
            }
            else if (order == "asc")
            {
                var transactionsAsc = _transactionDbContext.Transactions.OrderBy(x => x.CreatedAt).ToList();
                return transactionsAsc;
            }
            else
            {
               var transactionsDesc = _transactionDbContext.Transactions.OrderByDescending(x => x.CreatedAt).ToList();
                return transactionsDesc;
            }
        } 

        public List<TransactionDTO> GetTransactionsByUserId(string token)
        {
            var userId = _tokenUtil.VerifyToken(token);
             
            List<TransactionDTO> transactions = _transactionDbContext.Transactions.
                   Where(x => x.UserId == userId)
                   .ToList();

            if (transactions == null)
            {
                throw new TransactionNotFound("No transactions with that id");
            }

            return transactions;
        }

        public TransactionDTO CreateTransaction(string token, TransactionDTO transactionDTO)
        {
            var userId = _tokenUtil.VerifyToken(token);
            _transactionValidationService.TransactionFieldsValidation(token, transactionDTO, transactionDTO.SourceAccount);    
            _accountService.GetAccountByIdAndUserId(transactionDTO.SourceAccount, token);
            _accountService.GetAccount(transactionDTO.DestinationAccount);

            transactionDTO.CreatedAt = DateTime.Now;
            transactionDTO.UserId = userId;

            _transactionDbContext.Transactions.Add(transactionDTO);
            _transactionDbContext.SaveChanges();

            return transactionDTO;
        }

        public TransactionDTO ReverseTransaction(int transactionId)
        {
            var transaction = GetTransactionById(transactionId);

            TransactionDTO reverse = new TransactionDTO();

            reverse.SourceAccount = transaction.DestinationAccount;
            reverse.DestinationAccount = transaction.SourceAccount;
            reverse.Amount = transaction.Amount;
            reverse.CreatedAt = DateTime.Now;
            reverse.UserId = transaction.UserId;

            _transactionDbContext.Add(reverse);
            _transactionDbContext.SaveChanges();

            return reverse;
        }
    }

}
