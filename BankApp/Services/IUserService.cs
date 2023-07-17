using BankApp.Model;
using BankApp.Model.DTO;
using BankApp.Models;

namespace BankApp.Services
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
