using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Interfaces.Repositories;
using onboarding_dotnet.Interfaces.Services;
using onboarding_dotnet.Utils.Enums;

namespace onboarding_dotnet.Services;

public class TransactionService(
    ApplicationDBContext context,
    ITransactionRepository transactionRepository,
    ILogger<TransactionService> logger
): ITransactionService
{
    private readonly ApplicationDBContext _context = context;
    private readonly ITransactionRepository _transactionRepository = transactionRepository;

    private readonly ILogger<TransactionService> _logger = logger;

    public async Task<bool> UpdatePaymentStatusToSuccess(int transactionId)
    {
        // Begin transaction
        var dbTransaction = _context.Database.BeginTransaction();

        try
        {
            // Find the transaction with related order
            var transaction = await _transactionRepository.FindOneWithRelations(transactionId);

            if (transaction.PaymentStatus == PaymentStatus.Success)
            {
                throw new Exception("Transaction already paid.");
            }

            // Update the payment status to success
            transaction.PaymentStatus = PaymentStatus.Success;

            // Update the order status to paid
            transaction.Order.Status = OrderStatus.Processed;

            // Save the changes
            _context.Transactions.Update(transaction);
            _context.Orders.Update(transaction.Order);
            await _context.SaveChangesAsync();

            // Commit the transaction   
            dbTransaction.Commit();

            _logger.LogInformation("Transaction with id {transactionId} has been updated to success.", transactionId);

            return true;
        }
        catch (Exception ex)
        {
            // Rollback the transaction
            dbTransaction.Rollback();
            throw new Exception(ex.Message);
        }
    }
}