using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding_dotnet.Models;

[Table("products")]
public class Product: BaseModel
{
    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    
    [Column("price", TypeName = "decimal(12,2)")]
    public decimal Price { get; set; }

    [Column("stock")]
    public int Stock { get; set; }

    [Column("description")]
    [MaxLength(255)]
    public string? Description { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    // Relationship
    public virtual Category Category { get; set; } = null!;
}