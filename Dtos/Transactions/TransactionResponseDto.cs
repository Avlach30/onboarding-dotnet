using onboarding_dotnet.Dtos.Orders;

namespace onboarding_dotnet.Dtos.Transactions;

public class TransactionResponseDto
{
    public int Id { get; set; }

    public required string PaymentMethod { get; set; }

    public required string PaymentStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}