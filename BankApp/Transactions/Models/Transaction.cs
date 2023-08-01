using BankApp.Accounts.Models;
using BankApp.Users.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Transactions.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SourceAccount { get; set; }
        public int DestinationAccount { get; set; }

        public virtual Account AccountSource { get; set; }

        public virtual Account AccountDestination { get; set; }

        [Required]
        public double Amount { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey("UserIdTransaction")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
