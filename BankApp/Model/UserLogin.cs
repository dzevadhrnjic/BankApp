using System.Text.Json.Serialization;

namespace BankApp.Model
{
    public class UserLogin
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
