using BankApp.Model.DTO;
using BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Test",
                    LastName = "Test",
                    Address = "sfsff",
                    PhoneNumber = "Test",
                    Email = "Test",
                    CreatedAt = DateTime.Now,
                    Password = "Test",
                    VerifyEmail = false
                },
                 new User
                 {
                     Id = 2,
                     FirstName = "Test2",
                     LastName = "Test2",
                     Address = "sfsff",
                     PhoneNumber = "Test",
                     Email = "Test",
                     CreatedAt = DateTime.Now,
                     Password = "Test",
                     VerifyEmail = false
                 },
                  new User
                  {
                      Id = 3,
                      FirstName = "Test3",
                      LastName = "Test3",
                      Address = "sfsff",
                      PhoneNumber = "Test",
                      Email = "Test",
                      CreatedAt = DateTime.Now,
                      Password = "Test",
                      VerifyEmail = false
                  });
        }
    }
}
