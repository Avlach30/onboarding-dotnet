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

    public async Task<AsyncVoidMethodBuilder> Update(int id, CategoryRequestDto data)
    {
        var category = await _categoryRepository.FindOne(id);

        category.Name = data.Name;
        category.Description = data.Description;

        return await _categoryRepository.UpdateAsync(category);
    }

    public async Task<bool> Delete(int id)
    {
        await _categoryRepository.FindOne(id);

        return await _categoryRepository.Delete(id);
    }
}