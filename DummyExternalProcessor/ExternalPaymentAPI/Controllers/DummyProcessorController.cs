using Microsoft.AspNetCore.Mvc;
using ExternalPaymentAPI.Requests;
using ExternalPaymentAPI.Application.Services;

namespace ExternalPaymentAPI.Controllers;

[ApiController]
[Route("api/processor")]
public class DummyProcessorController : ControllerBase
{
    private readonly ILogger<DummyProcessorController> _logger;
    private readonly IPaymentProcessor _processor;

    public DummyProcessorController(ILogger<DummyProcessorController> logger, IPaymentProcessor processor)
    {
        _logger = logger;
        _processor = processor;
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessPayment([FromBody] ProcessorPaymentRequest request)
    {
        _logger.LogInformation($"[Processor] Processing transaction {request.TransactionId}...");
        var result = await _processor.ProcessAsync(request);
        return Ok(result);
    }
}