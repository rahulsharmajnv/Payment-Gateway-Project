using EcommerceBackend.Api.Data;
using EcommerceBackend.Api.Models;

namespace EcommerceBackend.Api.Services
{
    public class CheckoutService
    {
        private readonly AppDbContext _context;

        public CheckoutService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> InitiatePaymentAsync(CheckoutRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null) return Guid.Empty;

            var txn = new Transaction
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Amount = request.Amount,
                Status = "Pending",
                Date = DateTime.UtcNow
            };

            _context.Transactions.Add(txn);
            await _context.SaveChangesAsync();

            // Simulate gateway notification insert
            _context.Notifications.Add(new Notification
            {
                Id = Guid.NewGuid(),
                TransactionId = txn.Id,
                NewStatus = "Success",
                Processed = false
            });

            await _context.SaveChangesAsync();

            return txn.Id;
        }
    }
}