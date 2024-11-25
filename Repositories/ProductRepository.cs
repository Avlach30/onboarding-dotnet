using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Dtos.Index;
using onboarding_dotnet.Dtos.Products;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Mappers;
using onboarding_dotnet.Models;
using onboarding_dotnet.Utils.Helpers;

namespace onboarding_dotnet.Repositories;

public class ProductRepository(ApplicationDBContext context)
{
    private readonly ApplicationDBContext _context = context;

    public async Task<IndexResponse<ProductDto>> FindAllForIndex(IndexProductRequestDto request)
    {
        var datas = _context.Products.Include(product => product.Category).AsSplitQuery().AsQueryable();

        // Default order by created_at desc
        if (string.IsNullOrEmpty(request.OrderBy))
        {
            datas = datas.OrderByDescending(product => product.Created_at);
        }

        // Implement search
        if (!string.IsNullOrEmpty(request.Search))
        {
            datas = datas.Where(product => product.Name.Contains(request.Search));
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

        return IndexResponse<ProductDto>.Success(
            result.Select(product => product.ToDto()).ToList(),
            totalData,
            "Get products success", 
            request.Page, 
            request.PerPage
        );
    }

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

    public Task<int> Delete(Product product)
    {
        _context.Products.Remove(product);
        return _context.SaveChangesAsync();
    }

}