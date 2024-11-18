using onboarding_dotnet.Dtos.Transactions;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Mappers;

public static class TransactionMapper
{
    public static TransactionDto ToDto(this Transaction transaction)
    {
        return new TransactionDto
        {
            Id = transaction.Id,
            PaymentMethod = transaction.PaymentMethod,
            PaymentStatus = transaction.PaymentStatus,
            CreatedAt = transaction.Created_at,
            UpdatedAt = transaction.Updated_at
        };
    }

    public static TransactionResponseDto ToResponse(this Transaction transaction)
    {
        return new TransactionResponseDto
        {
            Id = transaction.Id,
            PaymentMethod = transaction.PaymentMethod,
            PaymentStatus = transaction.PaymentStatus,
            CreatedAt = transaction.Created_at,
            UpdatedAt = transaction.Updated_at
        };
    }
}