using onboarding_dotnet.Models;

namespace onboarding_dotnet.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> FindOneWithRelations(int id);

        Task<Order> FindOneWithoutRelations(int id);
    }
}