using IdentityService.Business.Abstracts;
using IdentityService.Business.Dtos.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService service) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequestDto request)
        {
            var result =await  service.Login(request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromQuery] string token)
        {
            var result = await service.Logout(token);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDto request)
        {
            var result = await service.Registration(request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromQuery]string token)
        {
            var result = await service.Refresh(token);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
