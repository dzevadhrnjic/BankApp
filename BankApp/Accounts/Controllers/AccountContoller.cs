using BankApp.Accounts.Exceptions;
using BankApp.Accounts.Models.DTO;
using BankApp.Accounts.Services;
using BankApp.Blacklist.Exceptions;
using BankApp.EmailVerification.Exceptions;
using BankApp.PdfFile.Services;
using BankApp.Transactions.Models.DTO;
using BankApp.Transactions.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Accounts.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountContoller : ControllerBase
    {

        private readonly IAccountService _accountService;
        private readonly IGeneratePdfService _generatePdfService;
        private readonly IAmountService _amountService;

        public AccountContoller(IAccountService accountService, IGeneratePdfService generatePdfService, IAmountService amountService)
        {
            _accountService = accountService;
            _generatePdfService = generatePdfService;
            _amountService = amountService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<AccountDTO>> GetAccounts(int pageSize, int pageNumber)
        {
            var result = _accountService.GetAllAccounts(pageSize, pageNumber);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AccountDTO> GetAccount(int id)
        {
            try
            {
                var result = _accountService.GetAccount(id);
                return Ok(result);
            }catch (AccountNotFound e)
            {
                return NotFound(e.Message);
            }   
        }

        [HttpGet("userId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<AccountDTO> GetAccountByUserId([FromHeader] string token)
        {
            try
            {
                var result = _accountService.GetAccountsByUserId(token);
                return Ok(result);
            }catch (AccountNotFound e)
            {
                return NotFound(e.Message);
            }catch (BlacklistTokenException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("idAndUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AccountDTO> GetAccountByIdAndUserId(int id, [FromHeader] string token)
        {
            try
            {
                var result = _accountService.GetAccountByIdAndUserId(id, token);
                return Ok(result);
            }catch (AccountNotFound e)
            {
                return NotFound(e.Message);
            }catch (BlacklistTokenException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AccountDTO> CreateAccount([FromHeader] string token, AccountDTO accountDto)
        {
            try
            {
                var result = _accountService.CreateAccount(token, accountDto);
                return CreatedAtRoute("", result);
            }
            catch (Exception e) when(
                   e is Exception ||
                   e is BlacklistTokenException
            ){
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteAccount([FromHeader] string token, int id)
        {
            try
            {
                _accountService.DeleteAccount(token, id);
                return NoContent();
            }catch (AccountNotFound e)
            {
                return NotFound(e.Message);
            }catch (BlacklistTokenException e )
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AccountDTO> UpdateAccount([FromHeader] string token, int id, AccountDTO accountDto)
        {
            try
            {
                _accountService.UpdateAccount(token, id, accountDto);
                return NoContent();
            } catch (AccountNotFound e)
            {
                return NotFound(e.Message);
            } catch (Exception e) when (
                e is Exception  ||
                e is BlacklistTokenException)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("balance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Balance> GetBalance(string token, int accountId)
        {
            try
            {
                var result = _amountService.GetBalance(token, accountId);
                return Ok(result);
            }
            catch (Exception e) when (
                e is Exception ||
                e is BlacklistTokenException)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("pdf/{accountId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Statement(string token, int accountId)
        {
            try
            {
                _generatePdfService.Statement(token, accountId);
                return Ok();
            } catch (AccountNotFound e)
            {
                return NotFound(e.Message);
            } catch (Exception e) when (
                    e is EmailVerificationException ||
                    e is BlacklistTokenException || 
                    e is Exception)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
