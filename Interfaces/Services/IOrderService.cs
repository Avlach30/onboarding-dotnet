using System.Runtime.CompilerServices;
using onboarding_dotnet.Dtos.Orders;

namespace onboarding_dotnet.Interfaces.Services
{
    public interface IOrderService
    {
        public Task<bool> CreateAsync(OrderRequestDto requestDto, int loggedUserId);
        public Task<AsyncVoidMethodBuilder> UpdateOrderStatus(int orderId, string status);
    }
}