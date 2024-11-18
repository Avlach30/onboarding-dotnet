using onboarding_dotnet.Dtos.OrderProducts;
using onboarding_dotnet.Dtos.Transactions;
using onboarding_dotnet.Dtos.Users;

namespace onboarding_dotnet.Dtos.Orders;

public class OrderResponseDto
{
    public int Id { get; set; }

    public required string Status { get; set; }

    public decimal TotalPrice { get; set; }

    public UserResponseDto? User { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<OrderProductResponseDto> OrderProducts { get; set; } = [];

    public TransactionResponseDto? Transaction { get; set; }
}