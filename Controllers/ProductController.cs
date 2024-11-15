using Microsoft.AspNetCore.Mvc;
using onboarding_dotnet.Dtos.Products;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Interfaces.Services;
using onboarding_dotnet.Mappers;

namespace onboarding_dotnet.Controllers;

[ApiController]
[Route("products")]
public class ProductController(IProductService productService) : Controller
{
    private readonly IProductService _productService = productService;

    [HttpGet]
    public async Task<ActionResult<IndexResponse<ProductResponseDto>>> Index()
    {
        var products = await _productService.GetAll();

        var result = products.Select(product => product.ToResponse()).ToList();

        return Ok(IndexResponse<ProductResponseDto>.Success(result, "Get products success"));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<ProductResponseDto>>> Show(int id)
    {
        var product = await _productService.GetOne(id);

        return Ok(ApiResponse<ProductResponseDto>.Success(product.ToResponse(), "Get product success"));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> Store([FromBody] ProductRequestDto productRequestDto)
    {
        await _productService.Create(productRequestDto);

        return Ok(ApiResponse.Success("Product created"));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] ProductRequestDto productRequestDto)
    {
        await _productService.Update(id, productRequestDto);

        return Ok(ApiResponse.Success("Product updated"));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse>> Delete(int id)
    {
        await _productService.Delete(id);

        return Ok(ApiResponse.Success("Product deleted"));
    }
}