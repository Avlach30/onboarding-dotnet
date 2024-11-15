using System.ComponentModel.DataAnnotations;

namespace onboarding_dotnet.Dtos.Products;
public class ProductRequestDto
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Name must be less than 100 characters")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "CategoryId is required")]
    [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be greater than 1")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public int Price { get; set; }

    [Required(ErrorMessage = "Stock is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Stock must be greater than 1")]
    public int Stock { get; set; }

    [MaxLength(255, ErrorMessage = "Description must be less than 255 characters")]
    public string? Description { get; set; }
}