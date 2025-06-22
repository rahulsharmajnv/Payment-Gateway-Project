using Microsoft.AspNetCore.Mvc;
using ExternalPaymentAPI.Requests;

namespace ExternalPaymentAPI.Controllers;

[ApiController]
[Route("api/bank")]
public class DummyBankController : ControllerBase
{
    private readonly ILogger<DummyBankController> _logger;

    public DummyBankController(ILogger<DummyBankController> logger)
    {
        _logger = logger;
    }

    [HttpPost("confirm")]
    public IActionResult Confirm([FromBody] BankPaymentRequest request)
    {
        var approved = new Random().Next(0, 10) > 2;
        _logger.LogInformation($"[Bank] Transaction {request.TransactionId} {(approved ? "APPROVED" : "DECLINED")}");

        return Ok(new { transactionId = request.TransactionId, status = approved ? "Approved" : "Declined" });
    }
}