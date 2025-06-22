using ExternalPaymentAPI.Requests;

namespace ExternalPaymentAPI.Application.Services;

public interface IPaymentProcessor
{
    Task<(Guid TransactionId, string Status)> ProcessAsync(ProcessorPaymentRequest request);
}