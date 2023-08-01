using BankApp.Blacklist.Exceptions;
using BankApp.ChangePassword.Models;
using BankApp.ChangePassword.Services;
using BankApp.EmailVerification.Exceptions;
using BankApp.EmailVerification.Models.DTO;
using BankApp.EmailVerification.Services;
using BankApp.Users.Exceptions;
using BankApp.Users.Models;
using BankApp.Users.Models.DTO;
using BankApp.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Users.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly ILoginService _loginService;
        private readonly IVerificationService _verificationService;
        private readonly IChangePasswordService _changePasswordService;

        public UserController(IUserService userService, ILoginService loginService,
            IVerificationService verificationService, IChangePasswordService changePasswordService)
        {
            _userService = userService;
            _loginService = loginService;
            _verificationService = verificationService;
            _changePasswordService = changePasswordService;
        }

        [HttpGet]
        public ActionResult<List<UserDTO>> GetUsers(int pageSize, int pageNumber)
        {
            var result = _userService.GetAllUsers(pageSize, pageNumber);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> GetUserById(int id)
        {
            try
            {
                var user = _userService.GetById(id);
                return Ok(user);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> CreateUser(UserDTO userDto)
        {
            try
            {
                var createUser = _userService.CreateUser(userDto);
                return Created("", createUser);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return NoContent();
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> UpdateUser(int id, UserDTO userDto)
        {
            try
            {
                var updateUser = _userService.UpdateUser(id, userDto);
                return NoContent();
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserLogin> Login(UserLogin userLogin)
        {
            try
            {
                var login = _loginService.Login(userLogin);
                return Ok(login);
            }
            catch (InvalidEmailOrPasswordException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("verifyEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VerificationDTO> VerifyEmail(VerificationDTO verificationDTO)
        {
            try
            {
                var result = _verificationService.VerifyEmail(verificationDTO);
                return Ok(result);
            }catch (EmailVerificationException e)
            {
                return BadRequest(e.Message);
            }catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("changePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult ChangePassword(string token)
        {
            try
            {
                _changePasswordService.PasswordChange(token);
                return Ok();
            }catch (BlacklistTokenException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("newPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PasswordChange> NewPassword(string token, PasswordChange passwordChange)
        {
            try
            {
                var result = _changePasswordService.NewPassword(token, passwordChange);
                return Ok(result);
            }
            catch (Exception e) when (
                e is InvalidEmailOrPasswordException ||
                e is BlacklistTokenException)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
