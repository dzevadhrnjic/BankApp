using BankApp.Transactions.Exceptions;
using BankApp.Transactions.Models.DTO;
using BankApp.Transactions.Services;

namespace BankApp.Transactions.Validations
{
    public class TransactionValidationService : ITransactionValidationService
    {

        private readonly IAmountService _amountService;

        public TransactionValidationService(IAmountService amountService)
        {
              _amountService = amountService;
        }

        public void TransactionFieldsValidation(string token, TransactionDTO transactionDTO, int accountId)
        {
            AmountFieldValidation(transactionDTO);
            SourceAccountValidation(token, accountId, transactionDTO);
        }

        public void AmountFieldValidation(TransactionDTO transactionDTO)
        {
            if (transactionDTO.Amount <= 0)
            {
                throw new ValidationException("Amount can't be zero or less");
            }
        }

        public void SourceAccountValidation(string token, int accountId, TransactionDTO transactionDTO)
        {
            var balance = _amountService.GetBalanceSource(accountId);

            if(balance < transactionDTO.Amount)
            {
                throw new ValidationException("You don't have amount on your account for transaction");
            }
        }
    }
}
