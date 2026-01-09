using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SureLbraryAPI.Context;
using SureLbraryAPI.DTOs;
using SureLbraryAPI.Interfaces;
using SureLbraryAPI.Repository;

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
            var transaction= await _transactionService.DeleteTransactionAsync(id);
            if(transaction.IsSuccess)
            {
                return Ok(transaction);
            }
            return NotFound(transaction);
        }
    }

}
    
