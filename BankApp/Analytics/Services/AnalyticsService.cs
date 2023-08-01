using BankApp.Analytics.Models;
using BankApp.Transactions.Data;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Analytics.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly TransactionDbContext _transactionDbContext;

        public AnalyticsService(TransactionDbContext transactionDbContext)
        {
            _transactionDbContext = transactionDbContext;
        }

        public List<Analytic> Analytics(int id, string type)
        {

            var analyticsDay = _transactionDbContext.Transactions
                .Where(x => x.SourceAccount == id)
                .GroupBy(t => t.CreatedAt.Date)
                .Select(a => new Analytic
                {
                    Date = a.Key.ToString("yyyy-MM-dd"),
                    Total = a.Sum(t => t.Amount)
                })
                .AsEnumerable()
                .ToList();


            var analyticsMonth = _transactionDbContext.Transactions
                 .Where(x => x.SourceAccount == id)
                 .GroupBy(t => new { t.CreatedAt.Year, t.CreatedAt.Month })
                 .Select(a => new Analytic
                 {
                     Date = $"{a.Key.Month:D2}-{a.Key.Year}",
                     Total = a.Sum(t => t.Amount)
                 })
                 .AsEnumerable()
                 .ToList();

            if (type.Equals("months"))
            {
                return analyticsMonth;
            }
            else
            {
                return analyticsDay;
            }
        }
    }
}