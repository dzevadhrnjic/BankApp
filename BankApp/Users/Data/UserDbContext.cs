using BankApp.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Users.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }


    }
}
