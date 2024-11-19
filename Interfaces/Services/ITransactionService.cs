namespace onboarding_dotnet.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<bool> UpdatePaymentStatusToSuccess(int transactionId);
    }
}