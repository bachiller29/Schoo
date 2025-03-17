using FinanzautoShool.Aplication.Services.Auth.Interface;
using FinanzautoShool.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FinanzautoShool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username == "admin" && request.Password == "admin")
            {
                var token = _authService.GenerateToken(request.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized("Usuario o contraseña incorrectos.");
        }
    }
}
