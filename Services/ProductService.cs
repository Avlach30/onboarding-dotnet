using System.Runtime.CompilerServices;
using onboarding_dotnet.Dtos.Products;
using onboarding_dotnet.Mappers;
using onboarding_dotnet.Models;
using onboarding_dotnet.Repositories;

namespace onboarding_dotnet.Services;

public class ProductService(
    ProductRepository productRepository,
    CategoryRepository categoryRepository
)
{
    private readonly ProductRepository _productRepository = productRepository;
    private readonly CategoryRepository _categoryRepository = categoryRepository;

    public async Task<List<Product>> GetAll()
    {
        return await _productRepository.FindAll();
    }

    public async Task<Product> GetOne(int id)
    {
        return await _productRepository.FindOne(id);
    }

    public async Task<AsyncVoidMethodBuilder> Create(ProductRequestDto data)
    {
        await _categoryRepository.FindOne(data.CategoryId);

        return await _productRepository.CreateAsync(data.ToModel());
    }

    public async Task<AsyncVoidMethodBuilder> Update(int id, ProductRequestDto data)
    {
        var product = await _productRepository.FindOne(id);
        await _categoryRepository.FindOne(data.CategoryId);

        product.Name = data.Name;
        product.Price = data.Price;
        product.Stock = data.Stock;
        product.Description = data.Description;
        product.CategoryId = data.CategoryId;

        return await _productRepository.UpdateAsync(product);
    }

    public async Task<bool> Delete(int id)
    {
        await _productRepository.FindOne(id);

        return await _productRepository.Delete(id);
    }
}