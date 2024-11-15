using onboarding_dotnet.Dtos.Products;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Mappers;

public static class ProductMapper
{
    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            CategoryId = product.CategoryId,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,
            Description = product.Description,
            Category = product.Category?.ToDto() ?? null,
            CreatedAt = product.Created_at,
            UpdatedAt = product.Updated_at
        };
    }

    public static ProductResponseDto ToResponse(this Product product)
    {
        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,
            Description = product.Description,
            Category = product.Category?.ToResponse() ?? null,
            CreatedAt = product.Created_at,
            UpdatedAt = product.Updated_at
        };
    }

    public static Product ToModel(this ProductRequestDto productDto)
    {
        return new Product
        {
            CategoryId = productDto.CategoryId,
            Name = productDto.Name,
            Price = productDto.Price,
            Stock = productDto.Stock,
            Description = productDto.Description
        };
    }
}