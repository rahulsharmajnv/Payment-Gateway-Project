namespace EcommerceBackend.Api.Models
{
    public class CheckoutRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
    }
}