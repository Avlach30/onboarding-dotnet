using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding_dotnet.Models;

[Table("users")]
public class User: BaseModel
{
    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Column("address")]
    [MaxLength(100)]
    public string Address { get; set; } = null!;

    [Column("email")]
    [MaxLength(100)]
    public string Email { get; set; } = null!;

    [Column("password")]
    [MaxLength(100)]
    public string Password { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = [];
}