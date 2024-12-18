using onboarding_dotnet.Dtos.Categories;

namespace onboarding_dotnet.Dtos.Products;

public class ProductResponseDto
{
    public int Id { get; set; }

    public string? Poster { get; set; }
    
    public required string Name { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string? Description { get; set; }

    public CategoryResponseDto? Category { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
