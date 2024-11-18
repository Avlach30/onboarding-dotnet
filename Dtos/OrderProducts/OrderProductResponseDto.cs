using onboarding_dotnet.Dtos.Orders;
using onboarding_dotnet.Dtos.Products;

namespace onboarding_dotnet.Dtos.OrderProducts;

public class OrderProductResponseDto
{
    public int Id { get; set; }

    public ProductResponseDto? Product { get; set; }

    public int Quantity { get; set; }
}