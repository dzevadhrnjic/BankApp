namespace BankApp.Exceptions
{
    public class InvalidEmailOrPasswordException : Exception
    {
        public InvalidEmailOrPasswordException(string message) : base(message) { }
    }
}
