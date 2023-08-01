using BankApp.Transactions.Models.DTO;

namespace BankApp.Transactions.Validations
{
    public interface ITransactionValidationService
    {
        void TransactionFieldsValidation(string token, TransactionDTO transactionDTO, int accountId);
    }
}
