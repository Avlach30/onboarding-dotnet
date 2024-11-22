using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Repositories;

public class CategoryRepository(ApplicationDBContext context)
{
    private readonly ApplicationDBContext _context = context;

    public async Task<List<Category>> FindAll()
    {
        return await _context.Categories.OrderByDescending(category => category.Created_at).ToListAsync();
    }

    public Task<Category> FindOne(int id)
    {
        var result = _context.Categories.Find(id) ?? throw new Exception("Category not found");

        return Task.FromResult(result);
    }

    public async Task<AsyncVoidMethodBuilder> CreateAsync(Category data)
    {
        await _context.Categories.AddAsync(data);
        await _context.SaveChangesAsync();

        return AsyncVoidMethodBuilder.Create();
    }

    public async Task<int> UpdateAsync(Category data)
    {
        _context.Categories.Update(data);
        return await _context.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        Category category = new() { Id = id };

        _context.Categories.Remove(category);
        return _context.SaveChangesAsync();
    }
}