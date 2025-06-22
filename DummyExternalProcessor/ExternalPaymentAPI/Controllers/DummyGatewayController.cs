using Microsoft.AspNetCore.Mvc;
using ExternalPaymentAPI.Requests;

namespace ExternalPaymentAPI.Controllers;

[ApiController]
[Route("api/gateway")]
public class DummyGatewayController : ControllerBase
{
    private readonly ILogger<DummyGatewayController> _logger;
    private readonly HttpClient _httpClient;

    public DummyGatewayController(ILogger<DummyGatewayController> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    [HttpPost("initiate")]
    public async Task<IActionResult> InitiatePayment([FromBody] GatewayPaymentRequest request)
    {
        _logger.LogInformation($"[Gateway] Received payment for {request.MerchantOrderId}, amount: {request.Amount}");

        var processorPayload = new ProcessorPaymentRequest
        {
            TransactionId = Guid.NewGuid(),
            Amount = request.Amount,
            CustomerEmail = request.CustomerEmail
        };

        var response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/processor/process", processorPayload);
        var result = await response.Content.ReadAsStringAsync();

        return Ok(new { processorPayload.TransactionId, ProcessorResponse = result });
    }
}