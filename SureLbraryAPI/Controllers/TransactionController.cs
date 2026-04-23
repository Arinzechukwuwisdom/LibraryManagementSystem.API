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
        private readonly ITransactionRepository _transactionRepository;
        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransactionAsync(
        int bookId,
        int userId,
        [FromBody] CreateTransactionDTO transactionDetails)
        {
            var req = await _transactionRepository.CreateTransactionAsync(bookId, userId, transactionDetails);
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
            var req = await _transactionRepository.GetAllTransactionsAsync();
            if (req.IsSuccess)
            {
                return Ok(req);
            }
            return NotFound(req);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionByIdAsync(int id)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
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
            var transaction = await _transactionRepository.DeleteTransactionAsync(id);
            if (transaction.IsSuccess)
            {
                return Ok(transaction);
            }
            return NotFound(transaction);
            }

        [HttpPatch]
        public async Task<IActionResult> UpdateTransactionAsync(CreateTransactionDTO transactionDetails, int id)
        {
            var transaction = await _transactionRepository.UpdateTransactionAsync(transactionDetails, id);
            if (transaction.IsSuccess) 
            {
                return Ok(transaction);
            }
            return NotFound(transaction);
        }

        [HttpGet("userId/{userId}")]
        public async Task<IActionResult> GetTransactionByUserIdAsync(int userId)
        {
            var transaction = await _transactionRepository.GetTransactionByUserIdAsync(userId);
            if (transaction.IsSuccess)
            {
                return Ok(transaction);
            }
            return NotFound(transaction);
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetTransactionByBookIdAsync(int bookId)
        {
            var transaction = await _transactionRepository.GetTransactionByBookIdAsync(bookId);
            if (transaction.IsSuccess)
            {
                return Ok(transaction);
            }
            return NotFound(transaction);
        }

        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdueTransactionAsync(int userId)
        {
            var transaction = await _transactionRepository.GetOverdueTransactionAsync(userId);
            if (transaction.IsSuccess)
            {
                return Ok(transaction);
            }
            return NotFound(transaction);
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetAllTransactionByStatusAsync(TransactionStatus status)
        {
            var transaction = await _transactionRepository.GetAllTransactionByStatusAsync(status);
            if (transaction.IsSuccess)
            {
                return Ok(transaction);
            }
            return Ok(transaction);
        }
    }

}

