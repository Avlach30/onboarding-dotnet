using onboarding_dotnet.Models;

namespace onboarding_dotnet.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> FindOneWithRelations(int id);
    }
}