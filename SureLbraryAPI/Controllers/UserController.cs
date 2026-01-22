using Microsoft.AspNetCore.Mvc;
using SureLbraryAPI.DTOs;
using SureLbraryAPI.Interfaces;

namespace SureLbraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userService;
        public UserController(IUserRepository userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(CreateUserDTO userDetail)
        {
            try
            {
                var req = await _userService.CreateUserAsync(userDetail);
                if (req.IsSuccess)
                {
                    return Ok(req.IsSuccess);
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

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var request = await _userService.DeleteUserAsync(id);
                if (request)
                {
                    return Ok(request);
                }
                else
                {
                    return NotFound(request);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id) 
        {
            try
            {
                var request = await _userService.GetUserByIdAsync(id);
                if(request.IsSuccess)
                return Ok(request);
                return NotFound(request);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsersAsync() 
        {
            try
            {
                var request= await _userService.GetAllUsersAsync();
                if (request.IsSuccess)
                return Ok(request);
                return NotFound(request);

            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUserAsync (CreateUserDTO userDetails, int id)
        {
            try
            {
                var request = await _userService.UpdateUsersAsync(userDetails,id); 
                if(request.IsSuccess)
                    return Ok(request);
                return NotFound(request);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}
