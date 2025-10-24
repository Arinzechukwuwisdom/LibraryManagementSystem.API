using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SureLbraryAPI.DTOs;
using SureLbraryAPI.Interfaces;

namespace SureLbraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUserAync(CreateUserDTO request)
        {
            try
            {
                var req = await _authService.RegisterAsync(request);
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
        [HttpPost("login")]
        public async Task<ActionResult<ResponseLoginDTO>> LoginUserAsync([FromBody] LoginUserDTO request)
        {
            try
            {
                var req = await _authService.LoginUserAsync(request);
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

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticateOnlyEndpoint()
        {
            return Ok("You are Authenticated!");
        }
        [Authorize(Roles="Admin")]
        [HttpGet("Admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("You are an Admin!");
        }
    }
}
