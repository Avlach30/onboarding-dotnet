using onboarding_dotnet.Dtos.OrderProducts;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Mappers;

public static class OrderProductMapper
{
    public static OrderProductDto ToDto(this OrderProduct orderProduct)
    {
        return new OrderProductDto
        {
            Id = orderProduct.Id,
            OrderId = orderProduct.OrderId,
            ProductId = orderProduct.ProductId,
            Quantity = orderProduct.Quantity
        };
    }

    public static OrderProductResponseDto ToResponse(this OrderProduct orderProduct)
    {
        return new OrderProductResponseDto
        {
            Id = orderProduct.Id,
            Product = orderProduct.Product?.ToResponse() ?? null,
            Quantity = orderProduct.Quantity
        };
    }

    public static OrderProduct ToModel(this OrderProductRequestDto orderProductDto)
    {
        return new OrderProduct
        {
            ProductId = orderProductDto.ProductId,
            Quantity = orderProductDto.Quantity
        };
    }
}