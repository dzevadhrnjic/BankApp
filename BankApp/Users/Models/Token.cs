namespace BankApp.Users.Models
{
    public class Token
    {
        public string AccessToken { get; set; }

        internal void UserToken(string userToken)
        {
            AccessToken = userToken;
        }
    }
}
