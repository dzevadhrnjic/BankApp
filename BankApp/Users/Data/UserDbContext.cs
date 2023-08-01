using BankApp.Users.Models;
using BankApp.Users.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Users.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<UserDTO> Users { get; set; }
    }
}
