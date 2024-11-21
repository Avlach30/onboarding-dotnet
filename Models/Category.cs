using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding_dotnet.Models;

[Table("categories")]
public class Category : BaseModel
{
    [MaxLength(100)]
    public required string Name { get; set; }
    
    [MaxLength(255)]
    public string? Description { get; set; }

    public ICollection<Product> Products { get; } = [];
}