using EcommerceBackend.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.Identity?.Name;
            if (userId == null) return Unauthorized();
            var user = await _userService.GetUserAsync(Guid.Parse(userId));
            return Ok(user);
        }
    }
}