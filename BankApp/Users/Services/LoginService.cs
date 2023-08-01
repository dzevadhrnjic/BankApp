using BankApp.Users.Data;
using BankApp.Users.Exceptions;
using BankApp.Users.Models;
using BankApp.Users.Utils;

namespace BankApp.Users.Services
{
    public class LoginService : ILoginService
    {
        HashUtils hashUtils = new HashUtils();

        private readonly UserDbContext _userContext;
        private readonly TokenUtil _tokenUtil;

        public LoginService(UserDbContext userDbContext, TokenUtil tokenUtil)
        {
            _userContext = userDbContext;
            _tokenUtil = tokenUtil;
        }

        public Token Login(UserLogin userLogin)
        {
            Token token = new Token();

            var loginUser = _userContext.Users.FirstOrDefault(x => x.Email == userLogin.Email
            && x.Password.Equals(hashUtils.HashPassword(userLogin.Password)));

            if (loginUser == null)
            {
                throw new InvalidEmailOrPasswordException("Wrong email or password, please try again");
            }
            else
            {
                int userId = loginUser.Id;
                token.UserToken(_tokenUtil.GenerateJWT(userId));
            }

            return token;
        }

    }
}
