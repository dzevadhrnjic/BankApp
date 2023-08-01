using BankApp.Transactions.Models.DTO;

namespace BankApp.Transactions.Services
{
    public interface ITransactionService
    {
        List<TransactionDTO> GetAllTransactions(int pageSize, int pageNumber);
        TransactionDTO GetTransactionById(int id);
        List<TransactionDTO> GetTransactions(string order, DateTime? dateFrom, DateTime? dateTo);
        List<TransactionDTO> GetTransactionsByUserId(string token);
        TransactionDTO CreateTransaction(string token, TransactionDTO transactionDTO);
        TransactionDTO ReverseTransaction(int transactionId);
    }
}
