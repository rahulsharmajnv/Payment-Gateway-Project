namespace EcommerceBackend.Api.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } // Pending, Success, Failed
        public DateTime Date { get; set; }

        public User User { get; set; }
    }
}