using System.Net.Http.Json;
using ExternalPaymentAPI.Requests;
using ExternalPaymentAPI.Infrastructure.Repositories;

namespace ExternalPaymentAPI.Application.Services;

public class PaymentProcessor : IPaymentProcessor
{
    private readonly HttpClient _httpClient;
    private readonly ITransactionRepository _transactionRepo;

    public PaymentProcessor(HttpClient httpClient, ITransactionRepository transactionRepo)
    {
        _httpClient = httpClient;
        _transactionRepo = transactionRepo;
    }

    public async Task<(Guid TransactionId, string Status)> ProcessAsync(ProcessorPaymentRequest request)
    {
        var bankPayload = new BankPaymentRequest
        {
            TransactionId = request.TransactionId,
            Amount = request.Amount
        };

        var response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/bank/confirm", bankPayload);
        var result = await response.Content.ReadFromJsonAsync<BankResponse>();

        var userId = await _transactionRepo.GetUserIdByEmailAsync(request.CustomerEmail);
        if (userId.HasValue)
        {
            var txn = new Entities.Transaction
            {
                Id = result.TransactionId,
                UserId = userId.Value,
                Amount = request.Amount,
                Status = result.Status,
                Date = DateTime.UtcNow
            };
            await _transactionRepo.SaveTransactionAsync(txn);
        }

        return (result.TransactionId, result.Status);
    }

    private class BankResponse
    {
        public Guid TransactionId { get; set; }
        public string Status { get; set; }
    }
}