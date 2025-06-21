using EcommerceBackend.Api.Data;
using EcommerceBackend.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Api.Services
{
    public class AccountService
{
    private readonly AppDbContext _context;

    public AccountService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<object> GetAccountInfo(Guid userId)
    {
        var txns = await _context.Transactions
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.Date)
            .ToListAsync();

        var balance = txns.Where(t => t.Status == "Success").Sum(t => t.Amount);

        return new
        {
            balance,
            transactions = txns.Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Status = t.Status,
                Date = t.Date
            })
        };
    }
}


}