namespace BankApp.Accounts.Exceptions
{
    public class AccountNotFound : Exception
    {
        public AccountNotFound(string message) : base(message) { }
    }
}
