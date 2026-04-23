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
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpPost]
        public async Task<IActionResult> AddBookAsync(CreateBookDTO bookDetail)
        {
            try
            {
                var req = await _bookRepository.AddBookAsync(bookDetail);
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
        public async Task<IActionResult> GetBookByAuthorAsync(string author)
        {
            try
            {
                var request = await _bookRepository.GetBookByAuthorAsync(author);
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
                var request = await _bookRepository.GetAllBooksAsync();
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
                var request = await _bookRepository.GetBookByIdAsync(id);
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
                var request= await _bookRepository.UpdateBookAsync(id, bookDetail);
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
                var request= await _bookRepository.DeleteBookAsync(id);
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
                var request= await _bookRepository.SearchBooksAsync(searchTerm);
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
                var request = await _bookRepository.GetAllBooksAsync();
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
                var request= await _bookRepository.GetBookBySpecificFilterAsync(filter);
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
