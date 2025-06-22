namespace ExternalPaymentAPI.Requests;

public class BankPaymentRequest
{
    public Guid TransactionId { get; set; }
    public decimal Amount { get; set; }
}