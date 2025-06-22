namespace ExternalPaymentAPI.Requests;

public class GatewayPaymentRequest
{
    public string MerchantOrderId { get; set; }
    public string CustomerEmail { get; set; }
    public decimal Amount { get; set; }
}