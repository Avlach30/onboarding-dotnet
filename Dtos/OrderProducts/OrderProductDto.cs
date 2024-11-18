using onboarding_dotnet.Dtos.Orders;
using onboarding_dotnet.Dtos.Products;

namespace onboarding_dotnet.Dtos.OrderProducts;

public class OrderProductDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public OrderDto? Order { get; set; }
    public int ProductId { get; set; }
    public ProductDto? Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

