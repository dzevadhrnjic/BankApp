using BankApp.EmailVerification.Models;
using BankApp.EmailVerification.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BankApp.EmailVerification.Data
{
    public class VerificationDbContext : DbContext
    {
        public VerificationDbContext(DbContextOptions<VerificationDbContext> options) : base(options) { }

        public DbSet<VerificationDTO > Verifications { get; set; }
    }
}
