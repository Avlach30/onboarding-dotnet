using System.Runtime.CompilerServices;
using onboarding_dotnet.Dtos.Categories;
using onboarding_dotnet.Dtos.Index;
using onboarding_dotnet.Infrastructures.Repositories;
using onboarding_dotnet.Mappers;
using onboarding_dotnet.Models;
using onboarding_dotnet.Repositories;

namespace onboarding_dotnet.Services;

public class CategoryService(CategoryRepository categoryRepository)
{
    private readonly CategoryRepository _categoryRepository = categoryRepository;

    public async Task<PaginationResult<Category>> GetAllForIndexPage(IndexCategoryRequestDto requestDto)
    {
        return await _categoryRepository.FindAllForIndex(requestDto);
    }

    public async Task<Category> GetOne(int id)
    {
        return await _categoryRepository.FindOneById(id) ?? throw new Exception("Category not found");
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