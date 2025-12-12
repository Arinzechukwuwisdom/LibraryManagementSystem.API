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
            var req=await _transactionService.CreateTransactionAsync(bookId, userId, transactionDetails);
            if (req == null)
            {
                return BadRequest(req);
            }
            else
            {
                return Ok(req);
            }
        }

        // Example GET for CreatedAtAction
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound(new { message = "Transaction not found" });
            }

            return Ok(transaction);
        }
    }

}
    
