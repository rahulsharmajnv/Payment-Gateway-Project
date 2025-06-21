using Microsoft.EntityFrameworkCore;
using EcommerceBackend.Api.Models; // Add this line if User is in the Models namespace

namespace EcommerceBackend.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}