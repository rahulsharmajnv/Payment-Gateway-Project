namespace ExternalPaymentAPI.Requests;

public class ProcessorPaymentRequest
{
    public Guid TransactionId { get; set; }
    public decimal Amount { get; set; }
    public string CustomerEmail { get; set; }
}