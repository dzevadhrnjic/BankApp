
using BankApp.Accounts.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Accounts.Data
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) { }

        public DbSet<AccountDTO> Accounts { get; set; }
    }
}
