using BankApp.Users.Models;
using BankApp.Users.Models.DTO;

namespace BankApp.Users.Services
{
    public interface IUserService
    {
        List<UserDTO> GetAllUsers(int pageSize, int pageNumber);
        UserDTO GetById(int id);
        UserDTO CreateUser(UserDTO userDto);
        void DeleteUser(int id);
        UserDTO UpdateUser(int id, UserDTO userDto);
    }
}
