using BankApp.PdfFile.Models;

namespace BankApp.PdfFile.Services
{
    public interface IGeneratePdfService
    {
        List<Statement> Statement(string token, int accountId);
    }
}
