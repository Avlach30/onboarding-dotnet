using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Interfaces.Repositories;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Repositories;

public class CategoryRepository(ApplicationDBContext context) : ICategoryRepository
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

    public async Task<AsyncVoidMethodBuilder> UpdateAsync(Category data)
    {
        _context.Categories.Update(data);
        await _context.SaveChangesAsync();

        return AsyncVoidMethodBuilder.Create();
    }

    public Task<bool> Delete(int id)
    {
        var data = FindOne(id).Result;

        _context.Categories.Remove(data);
        _context.SaveChanges();

        return Task.FromResult(true);
    }
}