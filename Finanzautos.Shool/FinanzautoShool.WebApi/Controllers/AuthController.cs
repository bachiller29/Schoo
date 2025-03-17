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

        /// <summary>
        /// Inicia sesión en el sistema y genera un token de autenticación.
        /// </summary>
        /// <param name="request">Objeto que contiene el nombre de usuario y contraseña.</param>
        /// <returns>
        /// Retorna un token JWT si las credenciales son correctas.  
        /// En caso contrario, devuelve un estado HTTP 401 (No autorizado).
        /// </returns>
        /// <response code="200">Autenticación exitosa. Se devuelve un token JWT.</response>
        /// <response code="401">Credenciales incorrectas. No autorizado.</response>
        /// <example>
        /// Ejemplo de petición JSON:
        /// {
        ///     "username": "admin",
        ///     "password": "admin"
        /// }
        /// </example>
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
