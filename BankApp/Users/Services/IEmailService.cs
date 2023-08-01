namespace BankApp.Users.Services
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
        void SendEmailWithAttachment(string to, string subject, string body);
    }
}
