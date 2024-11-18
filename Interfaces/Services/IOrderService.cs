using System.Runtime.CompilerServices;
using onboarding_dotnet.Dtos.Orders;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Interfaces.Services
{
    public interface IOrderService
    {
        Task<bool> CreateAsync(OrderRequestDto requestDto, int loggedUserId);

        Task<Order> GetOne(int id, bool withRelations = false);
        
        Task<AsyncVoidMethodBuilder> UpdateOrderStatus(int orderId, string status);
    }
}