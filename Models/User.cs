using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding_dotnet.Models;

[Table("users")]
public class User: BaseModel
{
    [Column("name")]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Column("address")]
    [MaxLength(100)]
    public required string Address { get; set; }

    [Column("email")]
    [MaxLength(100)]
    public required string Email { get; set; }

    [Column("password")]
    [MaxLength(100)]
    public required string Password { get; set; }

    public ICollection<Order> Orders { get; } = [];
}