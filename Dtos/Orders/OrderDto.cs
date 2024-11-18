using onboarding_dotnet.Dtos.Users;

namespace onboarding_dotnet.Dtos.Orders;

public class OrderDto
{
    public int Id { get; set; }
    public required string Status { get; set; }
    public decimal TotalPrice { get; set; }
    public int UserId { get; set; }
    public UserDto? User { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}