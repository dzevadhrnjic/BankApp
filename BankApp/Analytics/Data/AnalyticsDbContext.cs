using Microsoft.EntityFrameworkCore;

namespace BankApp.Analytics.Data
{
    public class AnalyticsDbContext : DbContext
    {
        public AnalyticsDbContext(DbContextOptions<AnalyticsDbContext> options) : base(options) { }
    }
}
