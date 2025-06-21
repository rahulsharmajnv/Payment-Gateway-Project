namespace EcommerceBackend.Api.Models
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }

}