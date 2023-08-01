namespace BankApp.EmailVerification.Models.DTO
{
    public class VerificationDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
