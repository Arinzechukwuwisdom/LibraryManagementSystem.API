using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SureLbraryAPI.DTOs;
using SureLbraryAPI.Interfaces;

namespace SureLbraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpPost]
        public async Task<IActionResult> AddBookAsync(CreateBookDTO bookDetail)
        {
            //try
            //{
                var req= await _bookService.AddBookAsync(bookDetail);
                if(req.IsSuccess)
                {
                    return Ok(req);
                }
                else
                {
                    return BadRequest(req);
                }
            //}
            //catch (Exception ex) 
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            //}
        }
            
    }
}
