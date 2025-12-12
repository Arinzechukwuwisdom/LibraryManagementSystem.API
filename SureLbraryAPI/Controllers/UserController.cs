using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using SureLbraryAPI.Context;
using SureLbraryAPI.DTOs;
using SureLbraryAPI.Interfaces;
using SureLbraryAPI.Utilities;

namespace SureLbraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
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
                var req = await _userService.DeleteUserAsync(id);
                if (req)  
                {
                    return Ok(req);
                }
                else
                {
                    return BadRequest(req);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}
