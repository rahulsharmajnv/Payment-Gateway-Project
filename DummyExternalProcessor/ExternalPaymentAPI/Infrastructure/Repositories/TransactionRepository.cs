using Microsoft.EntityFrameworkCore;
using ExternalPaymentAPI.Entities;
using ExternalPaymentAPI.Infrastructure.Data;

namespace ExternalPaymentAPI.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveTransactionAsync(Transaction txn)
    {
        _context.Transactions.Add(txn);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<Guid?> GetUserIdByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user?.Id;
    }
}