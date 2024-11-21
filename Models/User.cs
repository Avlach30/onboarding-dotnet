using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding_dotnet.Models;

[Table("users")]
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