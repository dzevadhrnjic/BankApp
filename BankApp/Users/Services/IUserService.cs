using BankApp.Users.Models;

namespace BankApp.Users.Services
{
    public interface IUserService
    {
        List<User> GetAllUsers(int pageSize, int pageNumber);
        User GetById(int id);
        User CreateUser(User user);
        void DeleteUser(int id);
        User UpdateUser(int id, User user);
    }
}
