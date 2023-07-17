using BankApp.Exceptions;
using BankApp.Models;

namespace BankApp.Validations
{
    public class UserValidationService
    {
        public static void UserFieldsValidation(User user) {

            ValidatePhoneNumber(user);
        }
        public static void ValidatePhoneNumber(User user)
        { 
            if(user.PhoneNumber.Length != 11)
            {
                throw new ValidationException("Enter field phone number with +, and 10 numbers");
            }else if(!user.PhoneNumber.StartsWith("+"))
            {
                throw new ValidationException("Enter phone number with +");      
            }
        }
    }
}
