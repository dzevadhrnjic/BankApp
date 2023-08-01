namespace BankApp.Transactions.Exceptions
{
    public class TransactionNotFound : Exception
    {
        public TransactionNotFound(string message) : base(message) { }
    }
}
