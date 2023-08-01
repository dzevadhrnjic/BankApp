using BankApp.Transactions.Models;
using BankApp.Users.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Accounts.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public double IntialBalance { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Transaction> TransactionsSource { get; set; }

        public virtual ICollection<Transaction> TransactionsDestination { get; set; }

    }
}
