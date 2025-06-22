using ExternalPaymentAPI.Entities;

namespace ExternalPaymentAPI.Infrastructure.Repositories;

public interface ITransactionRepository
{
    Task SaveTransactionAsync(Transaction txn);
    Task<bool> UserExistsAsync(string email);
    Task<Guid?> GetUserIdByEmailAsync(string email);
}