namespace BankApp.ChangePassword.Models
{
    public class PasswordChange
    {
        public string Code { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
