using System.ComponentModel.DataAnnotations;

namespace BankApp.Users.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(60)]
        public string LastName { get; set; }
       
        [Required]
        [MaxLength(60)]
        public string Address { get; set; }
        
        [Required]
        [MaxLength(60)]
        public string PhoneNumber { get; set; }
        
        [Required]
        [MaxLength(60)]
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required]
        [MaxLength(60)]
        public string Password { get; set; }
        public bool VerifyEmail { get; set; }
    }
}
