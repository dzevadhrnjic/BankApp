using BankApp.Accounts.Exceptions;
using BankApp.Blacklist.Exceptions;
using BankApp.Transactions.Exceptions;
using BankApp.Transactions.Models.DTO;
using BankApp.Transactions.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Transactions.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {

        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<TransactionDTO> GetAllTransactions(int pageSize, int pageNumber)
        {
            var result = _transactionService.GetAllTransactions(pageSize, pageNumber);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TransactionDTO> GetTransaction(int id)
        {
            try
            {
                var result = _transactionService.GetTransactionById(id);
                return Ok(result);
            } catch (TransactionNotFound e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("order")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TransactionDTO> GetTransactions(string order = "desc", DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            try
            {
                var result = _transactionService.GetTransactions(order, dateFrom, dateTo);
                return Ok(result);
            }catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("byUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<TransactionDTO>> GetTransactionsByUserId(string token)
        {
            try
            {
                var result = _transactionService.GetTransactionsByUserId(token);
                return Ok(result);
            }catch (TransactionNotFound e)
            {
                return NotFound(e.Message);
            }catch (BlacklistTokenException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TransactionDTO> CreateTransaction(string token, TransactionDTO transactionDTO)
        {
            try
            {
                var result = _transactionService.CreateTransaction(token, transactionDTO);
                return CreatedAtRoute("", result);
            }
            catch (AccountNotFound e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e) when(
                e is Exception ||
                e is BlacklistTokenException)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("reverse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TransactionDTO> ReverseTransaction(int transactionId)
        {
            try
            {
                var result = _transactionService.ReverseTransaction(transactionId);
                return Ok(result);
            }catch(TransactionNotFound e)
            {
                return NotFound(e.Message);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
