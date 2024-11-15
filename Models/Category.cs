using System.ComponentModel.DataAnnotations;

namespace onboarding_dotnet.Models;

public class Category : BaseModel
{
    [MaxLength(100)]
    public required string Name { get; set; }
    
    [MaxLength(255)]
    public string? Description { get; set; }
}