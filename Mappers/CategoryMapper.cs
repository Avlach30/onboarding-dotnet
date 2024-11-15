using onboarding_dotnet.Dtos.Categories;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Mappers;

public static class CategoryMapper
{
    public static CategoryDto ToDto(this Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            CreatedAt = category.Created_at,
            UpdatedAt = category.Updated_at
        };
    }

    public static CategoryResponseDto ToResponse(this Category category)
    {
        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            CreatedAt = category.Created_at,
            UpdatedAt = category.Updated_at
        };
    }

    public static Category ToModel(this CategoryRequestDto categoryRequestDto)
    {
        return new Category
        {
            Name = categoryRequestDto.Name,
            Description = categoryRequestDto.Description
        };
    }
}