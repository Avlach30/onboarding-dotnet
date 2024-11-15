using Microsoft.AspNetCore.Mvc;
using onboarding_dotnet.Dtos.Categories;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Interfaces.Repositories;
using onboarding_dotnet.Interfaces.Services;
using onboarding_dotnet.Mappers;

namespace onboarding_dotnet.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController(ICategoryService categoryService) : Controller
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet]
    public async Task<ActionResult<IndexResponse<CategoryResponseDto>>> Index()
    {
        var categories = await _categoryService.GetAll();

        var result = categories.Select(category => category.ToResponse()).ToList();

        return Ok(IndexResponse<CategoryResponseDto>.Success(result, "Get categories success"));
    }

    [HttpGet("{id}")]
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

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] CategoryRequestDto categoryRequestDto)
    {
        await _categoryService.Update(id, categoryRequestDto);

        return Ok(ApiResponse.Success("Category updated"));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> Delete(int id)
    {
        await _categoryService.Delete(id);

        return Ok(ApiResponse.Success("Category deleted"));
    }
}