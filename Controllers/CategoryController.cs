using Microsoft.AspNetCore.Mvc;
using onboarding_dotnet.Dtos.Categories;
using onboarding_dotnet.Dtos.Index;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Mappers;
using onboarding_dotnet.Services;

namespace onboarding_dotnet.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController(
    CategoryService categoryService
) : Controller
{
    private readonly CategoryService _categoryService = categoryService;

    [HttpGet]
    public async Task<ActionResult<IndexResponse<CategoryDto>>> Index(
        [FromQuery] IndexCategoryRequestDto request
    )
    {
        var result = await _categoryService.GetAllForIndexPage(request);

        return Ok(IndexResponse<CategoryDto>.Success(
            result.Data.Select(category => category.ToDto()).ToList(),
            result.Total,
            "Get categories success",
            request.Page,
            request.PerPage
        ));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<CategoryResponseDto>>> Show(int id)
    {
        var category = await _categoryService.GetOne(id);

        return Ok(ApiResponse<CategoryResponseDto>.Success(category.ToResponse(), "Get category success"));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> Store([FromBody] CategoryRequestDto categoryRequestDto)
    {
        await _categoryService.Create(categoryRequestDto);

        return Ok(ApiResponse.Success("Category created"));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] CategoryRequestDto categoryRequestDto)
    {
        await _categoryService.Update(id, categoryRequestDto);

        return Ok(ApiResponse.Success("Category updated"));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse>> Delete(int id)
    {
        await _categoryService.Delete(id);

        return Ok(ApiResponse.Success("Category deleted"));
    }
}