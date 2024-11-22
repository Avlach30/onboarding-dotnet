using System.Runtime.CompilerServices;
using onboarding_dotnet.Dtos.Products;
using onboarding_dotnet.Mappers;
using onboarding_dotnet.Models;
using onboarding_dotnet.Repositories;

namespace onboarding_dotnet.Services;

public class ProductService(ProductRepository productRepository)
{
    private readonly ProductRepository _productRepository = productRepository;

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
        return await _productRepository.CreateAsync(data.ToModel());
    }

    public async Task<int> Update(int id, ProductRequestDto data)
    {
        var updatedData = data.ToModel();
        updatedData.Id = id;

        return await _productRepository.UpdateAsync(updatedData);
    }

    public async Task<int> Delete(int id)
    {
        return await _productRepository.Delete(id);
    }
}