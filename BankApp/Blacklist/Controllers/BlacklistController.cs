using BankApp.Blacklist.Exceptions;
using BankApp.Blacklist.Models;
using BankApp.Blacklist.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Blacklist.Controllers
{
    [ApiController]
    [Route("api/blacklist/")]
    public class BlacklistController : ControllerBase
    {
        private readonly IBlacklistService _blacklistService;
        public BlacklistController(IBlacklistService blacklistService)
        {
            _blacklistService = blacklistService;
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BlacklistToken> Logout(string token)
        {
            try
            {
                var result = _blacklistService.Logout(token);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CheckToken(string token)
        {
            try
            {
                _blacklistService.BlacklistToken(token);
                return Ok();
            }
            catch (BlacklistTokenException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}