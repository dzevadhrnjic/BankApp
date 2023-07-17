using System.Security.Cryptography;
using System.Text;

namespace BankApp.Utils
{
    public class HashUtils
    {
        public string HashPassword(string password)
        {
           var sha = SHA256.Create();
           var asByteArray = Encoding.Default.GetBytes(password);
           var hashedPassword = sha.ComputeHash(asByteArray);
           return Convert.ToBase64String(hashedPassword);
        }
    }
}
