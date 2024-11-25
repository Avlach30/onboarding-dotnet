using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Dtos.Index;
using onboarding_dotnet.Infrastructures.Repositories;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Models;
using onboarding_dotnet.Utils.Helpers;

namespace onboarding_dotnet.Repositories;

public class CategoryRepository(ApplicationDBContext context)
{
    private readonly ApplicationDBContext _context = context;

    public async Task<PaginationResult<Category>> FindAllForIndex(IndexCategoryRequestDto request)
    {
        var datas = _context.Categories.AsQueryable();

        // Default order by created_at desc
        if (string.IsNullOrEmpty(request.OrderBy))
        {
            datas = datas.OrderByDescending(category => category.Created_at);
        }

        // Implement search
        if (!string.IsNullOrEmpty(request.Search))
        {
            datas = datas.Where(category => category.Name.Contains(request.Search));
        }

        int totalData = await datas.CountAsync();

        // Implement pagination
        datas = datas
            .Skip(PaginationHelper.GetSkip(
                request.Page, 
                request.PerPage
            ))
            .Take(request.PerPage);

        var result = await datas.ToListAsync();

        return new PaginationResult<Category>
        {
            Data = result,
            Total = totalData
        };
    }

    public async Task<List<Category>> FindAll()
    {
        return await _context.Categories.OrderByDescending(category => category.Created_at).ToListAsync();
    }

    public Task<Category?> FindOneById(int id)
    {
        var result = _context.Categories.FirstOrDefault(category => category.Id == id);

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