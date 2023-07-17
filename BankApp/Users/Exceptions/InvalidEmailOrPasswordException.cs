namespace BankApp.Users.Exceptions
{
    public class InvalidEmailOrPasswordException : Exception
    {
        public InvalidEmailOrPasswordException(string message) : base(message) { }
    }
}
