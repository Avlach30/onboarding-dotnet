using System.ComponentModel.DataAnnotations;

namespace onboarding_dotnet.Dtos.OrderProducts;

public class OrderProductRequestDto
{
    [Required(ErrorMessage = "ProductId is required")]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    public int Quantity { get; set; }
}