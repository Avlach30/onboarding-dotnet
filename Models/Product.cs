using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding_dotnet.Models;

public class Product: BaseModel
{
    [MaxLength(100)]
    public required string Name { get; set; }

    
    [Column(TypeName = "decimal(12,2)")]
    public decimal Price { get; set; }

    public int Stock { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    public int CategoryId { get; set; }

    public required Category Category { get; set; }
}