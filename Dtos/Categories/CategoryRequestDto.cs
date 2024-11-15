using System.ComponentModel.DataAnnotations;

namespace onboarding_dotnet.Dtos.Categories;

public class CategoryRequestDto
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Name must be less than 100 characters")]
    public required string Name { get; set; }

    [MaxLength(255, ErrorMessage = "Description must be less than 255 characters")]
    public string? Description { get; set; }
}