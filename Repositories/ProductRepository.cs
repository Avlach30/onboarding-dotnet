using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Repositories;

public class ProductRepository(ApplicationDBContext context)
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

    public async Task<int> UpdateAsync(Product data)
    {
        _context.Products.Update(data);
        return await _context.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        Product product = new(){ Id = id };

        _context.Products.Remove(product);
        return _context.SaveChangesAsync();
    }

}