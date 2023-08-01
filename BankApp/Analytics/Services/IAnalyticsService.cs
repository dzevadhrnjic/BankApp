using BankApp.Analytics.Models;

namespace BankApp.Analytics.Services
{
    public interface IAnalyticsService
    {
        List<Analytic> Analytics(int id, string type);
    }
}
