using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Dtos.Index;
using onboarding_dotnet.Infrastructures.Repositories;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Models;
using onboarding_dotnet.Utils.Helpers;

namespace onboarding_dotnet.Repositories;

public class OrderRepository(ApplicationDBContext context)
{
    private readonly ApplicationDBContext _context = context;

    public async Task<PaginationResult<Order>> FindAllForIndex(IndexOrderRequestDto request)
    {
        var datas = _context.Orders.Include(order => order.User).AsSplitQuery().AsQueryable();

        // Default order by created_at desc
        if (string.IsNullOrEmpty(request.OrderBy))
        {
            datas = datas.OrderByDescending(order => order.Created_at);
        }

        // Implement filter total price
        if (request.TotalPriceMin > 0 && request.TotalPriceMax > 0)
        {
            datas = datas.Where(order => order.TotalPrice >= request.TotalPriceMin && order.TotalPrice <= request.TotalPriceMax);
        } else if (request.TotalPriceMin > 0)
        {
            datas = datas.Where(order => order.TotalPrice >= request.TotalPriceMin);
        } else if (request.TotalPriceMax > 0)
        {
            datas = datas.Where(order => order.TotalPrice <= request.TotalPriceMax);
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

        return new PaginationResult<Order>
        {
            Data = result,
            Total = totalData
        };
    }

    public Task<Order?> FindOneByIdWithRelations(int id)
    {
        var result = _context.Orders
            .Include(order => order.User)
            .Include(order => order.Transaction)
            .Include(order => order.OrderProducts)
            .ThenInclude(orderProduct => orderProduct.Product)
            .AsSplitQuery()
            .FirstOrDefault(order => order.Id == id);

        return Task.FromResult(result);
    }

    public Task<Order?> FindOneByIdWithoutRelations(int id)
    {
        var result = _context.Orders
            .FirstOrDefault(order => order.Id == id);

        return Task.FromResult(result);
    }
}