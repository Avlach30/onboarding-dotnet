using onboarding_dotnet.Dtos.Orders;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Mappers;

public static class OrderMapper
{
    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            TotalPrice = order.TotalPrice,
            Status = order.Status,
            User = order.User?.ToDto() ?? null,
            CreatedAt = order.Created_at,
            UpdatedAt = order.Updated_at
        };
    }

    public static OrderResponseDto ToResponse(this Order order)
    {
        return new OrderResponseDto
        {
            Id = order.Id,
            Status = order.Status,
            TotalPrice = order.TotalPrice,
            User = order.User?.ToResponse() ?? null,
            OrderProducts = order.OrderProducts?.Select(orderProduct => orderProduct.ToResponse()).ToList() ?? [],
            Transaction = order.Transaction?.ToResponse() ?? null,
            CreatedAt = order.Created_at,
            UpdatedAt = order.Updated_at
        };
    }
}