using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding_dotnet.Models;

[Table("categories")]
public class Category : BaseModel
{   
    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    
    [Column("description")]
    [MaxLength(255)]
    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; } = [];
}