using BankApp.Accounts.Models.DTO;
using BankApp.Transactions.Models;
using BankApp.Transactions.Models.DTO;
using BankApp.Users.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Transactions.Data
{
    public class TransactionDbContext : DbContext
    {

        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options) { }
        
        public DbSet<TransactionDTO> Transactions { get; set; }

        public DbSet<AccountDTO> Accounts { get; set; }

        public DbSet<UserDTO> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(a => a.AccountSource)
                .WithMany(t => t.TransactionsSource)
                .HasForeignKey(a => a.SourceAccount)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transaction>()
               .HasOne(a => a.AccountDestination)
               .WithMany(t => t.TransactionsDestination)
               .HasForeignKey(a => a.DestinationAccount)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
