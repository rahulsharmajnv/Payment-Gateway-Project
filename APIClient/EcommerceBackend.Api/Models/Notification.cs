namespace EcommerceBackend.Api.Models
{
    public class Notification
    {
        public Guid Id { get; set; }
        public Guid TransactionId { get; set; }
        public string NewStatus { get; set; }
        public bool Processed { get; set; }
    }
}