using System.ComponentModel.DataAnnotations;
using onboarding_dotnet.Dtos.OrderProducts;

namespace onboarding_dotnet.Dtos.Orders;

public class OrderRequestDto
{
    [Required(ErrorMessage = "OrderProducts is required")]
    [MinLength(1, ErrorMessage = "OrderProducts must have at least one item")]
    public List<OrderProductRequestDto> OrderProducts { get; set; } = [];
}