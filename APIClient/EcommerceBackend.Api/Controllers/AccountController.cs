using EcommerceBackend.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Api.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }
   [Authorize]
    [HttpGet("info")]
    public async Task<IActionResult> GetInfo()
    {
        var userId = Guid.Parse(User.Identity?.Name);
        var result = await _accountService.GetAccountInfo(userId);
        return Ok(result);
    }
}
}