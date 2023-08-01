namespace BankApp.PdfFile.Models
{
    public class Statement
    {
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; }
    }
}
