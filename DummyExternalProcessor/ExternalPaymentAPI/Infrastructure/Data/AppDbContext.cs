using Microsoft.EntityFrameworkCore;
using ExternalPaymentAPI.Entities;

namespace ExternalPaymentAPI.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<User> Users { get; set; } // Add this if you have a User entity
}