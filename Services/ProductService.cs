using System.Runtime.CompilerServices;
using onboarding_dotnet.Dtos.Index;
using onboarding_dotnet.Dtos.Products;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Mappers;
using onboarding_dotnet.Models;
using onboarding_dotnet.Repositories;
using onboarding_dotnet.Utils.Helpers;

namespace onboarding_dotnet.Services;

public class ProductService(
    ProductRepository productRepository,
    IHttpContextAccessor httpContextAccessor
)
{
    private readonly ProductRepository _productRepository = productRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<IndexResponse<ProductDto>> GetAllForIndexPage(IndexProductRequestDto request)
    {
        return await _productRepository.FindAllForIndex(request);
    }

    public async Task<List<Product>> GetAll()
    {
        return await _productRepository.FindAll();
    }

    public async Task<Product> GetOne(int id)
    {
        return await _productRepository.FindOneByIdWithCategory(id) ?? throw new Exception("Data not found");
    }

    public async Task<AsyncVoidMethodBuilder> Create(ProductRequestDto data)
    {   
        // Validate file
        FileUpload.Validate(data.Poster, [".jpg", ".jpeg", ".png"], 1 * 1024 * 1024);

        // Get base url
        var baseUrl = GetBaseUrl.Get(_httpContextAccessor);

        // Process file upload and get path
        var path = await FileUpload.Upload(data.Poster, baseUrl);
        data.PosterPath = path;

        return await _productRepository.CreateAsync(data.ToModel());
    }

    public async Task<int> Update(int id, ProductRequestDto data)
    {
        // Validate file
        FileUpload.Validate(data.Poster, [".jpg", ".jpeg", ".png"], 1 * 1024 * 1024);

        // Check if in existing data there is a poster
        var existingData = await _productRepository.FindOneByIdWithCategory(id) ?? throw new Exception("Data not found");
        if (existingData.Poster != null)
        {
            // Delete existing file
            FileUpload.Delete(existingData.Poster);
        }

        // Get base url
        var baseUrl = GetBaseUrl.Get(_httpContextAccessor);

        // Process file upload and get path
        var path = await FileUpload.Upload(data.Poster, baseUrl);

        // Update data
        existingData.Poster = path;
        existingData.Name = data.Name;
        existingData.Price = data.Price;
        existingData.Stock = data.Stock;
        existingData.Description = data.Description;
        existingData.CategoryId = data.CategoryId;
        return await _productRepository.UpdateAsync(existingData);
    }

    public async Task<int> Delete(int id)
    {
        var existingData = await _productRepository.FindOneByIdWithCategory(id) ?? throw new Exception("Data not found");
        if (existingData.Poster != null)
        {
            // Delete existing file
            FileUpload.Delete(existingData.Poster);
        }

        return await _productRepository.Delete(existingData);
    }
}