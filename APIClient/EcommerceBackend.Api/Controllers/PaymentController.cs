namespace EcommerceBackend.Api.Controllers
{
    using System.Security.Claims;
    using EcommerceBackend.Api.Data;
    using EcommerceBackend.Api.Models;
    using EcommerceBackend.Api.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaymentController(AppDbContext context)
        {
            _context = context;
        }

        // Get all transactions for current user
        [HttpGet("transactions")]
        public async Task<IActionResult> GetUserTransactions()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized();

            var userId = Guid.Parse(userIdClaim);
            var txns = await _context.Transactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();

            var result = txns.Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Status = t.Status,
                Date = t.Date
            });

            return Ok(result);
        }

        // Get transaction by ID
        [HttpGet("transactions/{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            var txn = await _context.Transactions.FindAsync(id);
            if (txn == null) return NotFound();

            return Ok(new TransactionDto
            {
                Id = txn.Id,
                Amount = txn.Amount,
                Status = txn.Status,
                Date = txn.Date
            });
        }

        // ✅ Payment status by transaction ID
        [HttpGet("payment-status/{transactionId}")]
        public async Task<IActionResult> GetPaymentStatus(Guid transactionId)
        {
            var txn = await _context.Transactions.FindAsync(transactionId);
            if (txn == null) return NotFound("Transaction not found");

            return Ok(new
            {
                transactionId = txn.Id,
                status = txn.Status,
                amount = txn.Amount,
                date = txn.Date
            });
        }
    }
}