using System.ComponentModel.DataAnnotations;
using onboarding_dotnet.Dtos.OrderProducts;
using onboarding_dotnet.Utils.Enums;
using onboarding_dotnet.Utils.Validations;

namespace onboarding_dotnet.Dtos.Orders;

public class OrderRequestDto
{
    [Required(ErrorMessage = "PaymentMethod is required")]
    [ContainsStrings([PaymentMethodType.BankTransfer, PaymentMethodType.CreditCard, PaymentMethodType.EWallet])]
    public required string PaymentMethod { get; set; }

    [Required(ErrorMessage = "OrderProducts is required")]
    [MinLength(1, ErrorMessage = "OrderProducts must have at least one item")]
    public List<OrderProductRequestDto> OrderProducts { get; set; } = [];
}