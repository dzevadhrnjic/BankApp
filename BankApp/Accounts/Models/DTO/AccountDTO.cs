using BankApp.Users.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankApp.Accounts.Models.DTO
{
    public class AccountDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public double IntialBalance { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
