using EcommerceBackend.Api.Models;
using EcommerceBackend.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly CheckoutService _checkoutService;

        public CheckoutController(CheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [Authorize]
        [HttpPost("initiate-payment")]
        public async Task<IActionResult> Initiate(CheckoutRequest request)
        {
            var transactionId = await _checkoutService.InitiatePaymentAsync(request);
            return Ok(new { transactionId });
        }
    }
}