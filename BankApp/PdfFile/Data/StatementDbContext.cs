using BankApp.Accounts.Models.DTO;
using BankApp.Transactions.Models.DTO;
using BankApp.Users.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BankApp.PdfFile.Data
{
    public class StatementDbContext : DbContext
    {
        public StatementDbContext(DbContextOptions<StatementDbContext> options) : base(options) { } 

        public DbSet<TransactionDTO> Transactions { get; set; }
        public DbSet<AccountDTO> Accounts { get; set; }
        public DbSet<UserDTO> Users { get; set; }
    }
}

