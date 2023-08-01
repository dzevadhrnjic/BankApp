using System.ComponentModel.DataAnnotations;

namespace BankApp.Transactions.Models.DTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int SourceAccount { get; set; }
        public int DestinationAccount { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public double Amount { get; set; }
    }
}
