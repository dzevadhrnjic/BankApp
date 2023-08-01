using BankApp.Blacklist.Models;
using BankApp.Blacklist.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Blacklist.Data
{
    public class BlacklistDbContext : DbContext 
    {
        public BlacklistDbContext(DbContextOptions<BlacklistDbContext> options) : base(options) { }

        public DbSet<BlacklistTokenDTO> BlacklistTokens { get; set; }
    }
}
