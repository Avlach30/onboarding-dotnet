using System.ComponentModel.DataAnnotations;

namespace onboarding_dotnet.Models;

public class User: BaseModel
{
    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(100)]
    public required string Address { get; set; }

    [MaxLength(100)]
    public required string Email { get; set; }

    [MaxLength(100)]
    public required string Password { get; set; }

    public ICollection<Order> Orders { get; } = [];
}