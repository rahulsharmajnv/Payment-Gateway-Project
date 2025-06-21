using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
// Add the following using directives if these types are in different namespaces

 using EcommerceBackend.Api.Models;
using System.Security.Principal;
namespace EcommerceBackend.Api.Services
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (string.IsNullOrEmpty(result.Token)) return Unauthorized("Invalid credentials");
            return Ok(new { Token= result.Token, Message = "Login successful" });
        }
    }
}