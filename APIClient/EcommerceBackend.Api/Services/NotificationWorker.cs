using EcommerceBackend.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Api.Services
{
    public class NotificationWorker : BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly ILogger<NotificationWorker> _logger;

    public NotificationWorker(IServiceProvider provider, ILogger<NotificationWorker> logger)
    {
        _provider = provider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _provider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var unprocessed = await db.Notifications
                .Where(n => !n.Processed)
                .Take(10)
                .ToListAsync();

            foreach (var note in unprocessed)
            {
                var txn = await db.Transactions.FindAsync(note.TransactionId);
                if (txn != null)
                {
                    txn.Status = note.NewStatus;
                    note.Processed = true;
                    await db.SaveChangesAsync();
                    _logger.LogInformation($"Updated transaction {txn.Id} to {note.NewStatus}");
                }
            }

            await Task.Delay(5000, stoppingToken); // every 5 sec
        }
    }
}
}