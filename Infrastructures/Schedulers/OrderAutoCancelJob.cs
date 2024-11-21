using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Utils.Enums;
using Quartz;

namespace onboarding_dotnet.Infrastructures.Schedulers;

public class OrderAutoCancelJob(
    IServiceProvider serviceProvider,
    ILogger<OrderAutoCancelJob> logger
): IJob
{
    private readonly ApplicationDBContext _dBContext = serviceProvider.GetRequiredService<ApplicationDBContext>();
    private readonly ILogger<OrderAutoCancelJob> _logger = logger;

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("OrderAutoCancelJob is running...");

        // Get orders where status is Draft
        var orders = await _dBContext.Orders
            .Include(o => o.Transaction)
            .Where(o => o.Status == OrderStatus.Draft)
            .ToListAsync();

        // Filter orders that are created more than a day ago (24 hours) with LINQ
        orders = orders.Where(o => o.Created_at < DateTime.Now.AddDays(-1)).ToList();

        // Iterate through orders and cancel them
        foreach (var order in orders)
        {
            order.Status = OrderStatus.Cancelled;
            order.Updated_at = DateTime.Now;

            // If order has a transaction, update its status to Cancelled
            if (order.Transaction != null)
            {
                order.Transaction.PaymentStatus = PaymentStatus.Failed;
                order.Transaction.Updated_at = DateTime.Now;
            }
        }

        await _dBContext.SaveChangesAsync();

        _logger.LogInformation("Updated {0} orders", orders.Count);
    }
}