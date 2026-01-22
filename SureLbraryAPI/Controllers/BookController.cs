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
            try
            {
                var req = await _bookService.AddBookAsync(bookDetail);
            if (req.IsSuccess)
            {
                return Ok(req);
            }
            else
            {
                return BadRequest(req);
            }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("by-author")]
        public async Task<IActionResult> GetBookByNameAsynnc(string authorName)
        {
            try
            {
                var request = await _bookService.GetBookByNameAsync(authorName);
                if (request.IsSuccess)
                    return Ok(request);
                return BadRequest(request);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBookAsync() 
        {
            try
            {
                var request = await _bookService.GetAllBooksAsync();
                if(request.IsSuccess)
                    return Ok(request);
                return BadRequest(request);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookByIdAsync(int id) 
        {
            try
            {
                var request = await _bookService.GetBookByIdAsync(id);
                if (request.IsSuccess)
                    return Ok(request);
                return BadRequest(request);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateBookAsync(CreateBookDTO bookDetail, int id) 
        {
            try
            {
                var request= await _bookService.UpdateBookAsync(id, bookDetail);
                if(request.IsSuccess)
                    return Ok(request);
                return BadRequest(request);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) 
        {
            try
            {
                var request= await _bookService.DeleteBookAsync(id);
                if(request.IsSuccess)
                    return Ok(request);
                return BadRequest(request);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("searchBook")]
        public async Task<IActionResult> SearchBooksAsync(string searchTerm) 
        {
            try
            {
                var request= await _bookService.SearchBooksAsync(searchTerm);
                if(request.IsSuccess)
                    return Ok(request);
                return BadRequest(request);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableBookAsync() 
        {
            try
            {
                var request = await _bookService.GetAllBooksAsync();
                if(request.IsSuccess)
                    return Ok(request);
                return BadRequest(request);
            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetBookBySpecificFilterAsync(string filter)
        {
            try
            {
                var request= await _bookService.GetBookBySpecificFilterAsync(filter);
                if(request.IsSuccess)
                    return Ok(request);
                return BadRequest(request);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
