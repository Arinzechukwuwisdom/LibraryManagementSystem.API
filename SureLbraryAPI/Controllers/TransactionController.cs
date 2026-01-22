using Microsoft.AspNetCore.Mvc;
using SureLbraryAPI.DTOs;
using SureLbraryAPI.Enums;
using SureLbraryAPI.Interfaces;

namespace LbraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransactionAsync(
        int bookId,
        int userId,
        [FromBody] CreateTransactionDTO transactionDetails)
        {
            var req = await _transactionService.CreateTransactionAsync(bookId, userId, transactionDetails);
            if (req.IsSuccess)
            {
                return Ok(req);
                //return BadRequest(req);
            }
            else
            {
                return NotFound(req);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTransactionsAsync()
        {
            var req = await _transactionService.GetAllTransactionsAsync();
            if (req.IsSuccess)
            {
                return Ok(req);
            }
            return NotFound(req);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionByIdAsync(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction.IsSuccess)
            {
                // return BadRequest();
                return Ok(transaction);
            }
            return NotFound(transaction);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionAsync(int id)
        {
            var transaction = await _transactionService.DeleteTransactionAsync(id);
            if (transaction.IsSuccess)
            {
                return Ok(transaction);
            }
            return NotFound(transaction);
            }

        [HttpPatch]
        public async Task<IActionResult> UpdateTransactionAsync(CreateTransactionDTO transactionDetails, int id)
        {
            var transaction = await _transactionService.UpdateTransactionAsync(transactionDetails, id);
            if (transaction.IsSuccess) 
            {
                return Ok(transaction);
            }
            return NotFound(transaction);
        }

        [HttpGet("userId/{userId}")]
        public async Task<IActionResult> GetTransactionByUserIdAsync(int userId)
        {
            var transaction = await _transactionService.GetTransactionByUserIdAsync(userId);
            if (transaction.IsSuccess)
            {
                return Ok(transaction);
            }
            return NotFound(transaction);
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetTransactionByBookIdAsync(int bookId)
        {
            var transaction = await _transactionService.GetTransactionByBookIdAsync(bookId);
            if (transaction.IsSuccess)
            {
                return Ok(transaction);
            }
            return NotFound(transaction);
        }

        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdueTransactionAsync(int userId)
        {
            var transaction = await _transactionService.GetOverdueTransactionAsync(userId);
            if (transaction.IsSuccess)
            {
                return Ok(transaction);
            }
            return NotFound(transaction);
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetAllTransactionByStatusAsync(TransactionStatus status)
        {
            var transaction = await _transactionService.GetAllTransactionByStatusAsync(status);
            if (transaction.IsSuccess)
            {
                return Ok(transaction);
            }
            return Ok(transaction);
        }
    }

}

