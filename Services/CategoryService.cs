using System.Runtime.CompilerServices;
using onboarding_dotnet.Dtos.Categories;
using onboarding_dotnet.Mappers;
using onboarding_dotnet.Models;
using onboarding_dotnet.Repositories;

namespace onboarding_dotnet.Services;

public class CategoryService(CategoryRepository categoryRepository)
{
    private readonly CategoryRepository _categoryRepository = categoryRepository;

    public async Task<List<Category>> GetAll()
    {
        return await _categoryRepository.FindAll();
    }

    public async Task<Category> GetOne(int id)
    {
        return await _categoryRepository.FindOne(id);
    }

    public async Task<AsyncVoidMethodBuilder> Create(CategoryRequestDto data)
    {
        return await _categoryRepository.CreateAsync(data.ToModel());
    }

    public async Task<int> Update(int id, CategoryRequestDto data)
    {
        var updatedData = data.ToModel();
        updatedData.Id = id;

        return await _categoryRepository.UpdateAsync(updatedData);
    }

    public async Task<int> Delete(int id)
    {
        return await _categoryRepository.Delete(id);
    }
}