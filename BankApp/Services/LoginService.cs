using BankApp.Data;
using BankApp.Exceptions;
using BankApp.Model;
using BankApp.Utils;

namespace BankApp.Services
{
    public class LoginService : ILoginService
    {
        HashUtils hashUtils = new HashUtils();
        TokenUtil tokenUtil = new TokenUtil();

        private readonly UserDbContext _userContext;

        public LoginService(UserDbContext userDbContext)
        {
            _userContext = userDbContext;
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
                token.UserToken(tokenUtil.GenerateJWT(userId));
            }

            return token;
        }

    }
}
