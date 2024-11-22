using Microsoft.AspNetCore.Mvc;
using onboarding_dotnet.Dtos.Index;
using onboarding_dotnet.Dtos.Products;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Mappers;
using onboarding_dotnet.Services;

namespace onboarding_dotnet.Controllers;

[ApiController]
[Route("products")]
public class ProductController(
    ProductService productService
) : Controller
{
    private readonly ProductService _productService = productService;

    [HttpGet]
    public async Task<ActionResult<IndexResponse<ProductDto>>> Index(
        [FromQuery] IndexRequestDto request
    )
    {
        var result = await _productService.GetAllForIndexPage(request);

        return Ok(result);
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