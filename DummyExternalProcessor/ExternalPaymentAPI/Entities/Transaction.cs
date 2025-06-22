namespace ExternalPaymentAPI.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
}