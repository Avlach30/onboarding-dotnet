using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Infrastructures.Mails.Classes;
using onboarding_dotnet.Infrastructures.Mails.Interfaces;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Interfaces.Repositories;
using onboarding_dotnet.Interfaces.Services;
using onboarding_dotnet.Utils.Enums;

namespace onboarding_dotnet.Services;

public class TransactionService(
    ApplicationDBContext context,
    ITransactionRepository transactionRepository,
    ILogger<TransactionService> logger,
    IEmailService emailService
): ITransactionService
{
    private readonly ApplicationDBContext _context = context;
    private readonly ITransactionRepository _transactionRepository = transactionRepository;
    private readonly ILogger<TransactionService> _logger = logger;
    private readonly IEmailService _emailService = emailService;

    public async Task<bool> UpdatePaymentStatusToSuccess(int transactionId)
    {
        // Begin transaction
        var dbTransaction = await _context.Database.BeginTransactionAsync();

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
            await dbTransaction.CommitAsync();

            // Log the success message
            _logger.LogInformation("Transaction with id {transactionId} has been updated to success.", transactionId);

            // Send an email to the user
            EmailMetadata emailMetadata = new(
                transaction.Order.User.Email,
                "Payment Success",
                "Dear @Model.Name, your payment has been successfully processed."
            );

            EmailTemplateModel emailTemplateModel = new(
                transaction.Order.User.Name,
                transaction.Order.User.Email
            );

            // Send the email in a separate thread for performance
            _ = Task.Run(async () =>
            {
                await _emailService.SendUsingTemplate(emailMetadata, emailTemplateModel);;
            });

            return true;
        }
        catch (Exception ex)
        {
            // Rollback the transaction
            await dbTransaction.RollbackAsync();
            throw new Exception(ex.Message);
        }
    }
}