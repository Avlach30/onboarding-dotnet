using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Interfaces.Repositories;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Repositories;

public class ProductRepository(ApplicationDBContext context) : IProductRepository
{
    private readonly ApplicationDBContext _context = context;

    public async Task<List<Product>> FindAll()
    {
        var datas = _context.Products.Include(product => product.Category).AsSplitQuery().OrderByDescending(product => product.Created_at);

        return await datas.ToListAsync();
    }

    public Task<Product> FindOne(int id)
    {
        var result = _context.Products.Include(product => product.Category).AsSplitQuery().FirstOrDefault(product => product.Id == id) ?? throw new Exception("Product not found");

        return Task.FromResult(result);
    }

    public async Task<AsyncVoidMethodBuilder> CreateAsync(Product data)
    {
        await _context.Products.AddAsync(data);
        await _context.SaveChangesAsync();

        return AsyncVoidMethodBuilder.Create();
    }

    public async Task<AsyncVoidMethodBuilder> UpdateAsync(Product data)
    {
        _context.Products.Update(data);
        await _context.SaveChangesAsync();

        return AsyncVoidMethodBuilder.Create();
    }

    public Task<bool> Delete(int id)
    {
        var data = FindOne(id).Result;

        _context.Products.Remove(data);
        _context.SaveChanges();

        return Task.FromResult(true);
    }

}