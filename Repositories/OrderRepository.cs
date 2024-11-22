using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Dtos.Index;
using onboarding_dotnet.Dtos.Orders;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Mappers;
using onboarding_dotnet.Models;
using onboarding_dotnet.Utils.Helpers;

namespace onboarding_dotnet.Repositories;

public class OrderRepository(ApplicationDBContext context)
{
    private readonly ApplicationDBContext _context = context;

    public async Task<IndexResponse<OrderDto>> FindAllForIndex(IndexRequestDto request)
    {
        var datas = _context.Orders.Include(order => order.User).AsSplitQuery().AsQueryable();

        // Default order by created_at desc
        if (string.IsNullOrEmpty(request.OrderBy))
        {
            datas = datas.OrderByDescending(order => order.Created_at);
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

        return IndexResponse<OrderDto>.Success(
            result.Select(order => order.ToDto()).ToList(),
            totalData,
            "Get orders success", 
            request.Page, 
            request.PerPage
        );
    }

    public Task<Order> FindOneWithRelations(int id)
    {
        var result = _context.Orders
            .Include(order => order.User)
            .Include(order => order.Transaction)
            .Include(order => order.OrderProducts)
            .ThenInclude(orderProduct => orderProduct.Product)
            .AsSplitQuery()
            .FirstOrDefault(order => order.Id == id) 
            ?? throw new Exception("Order not found");

        return Task.FromResult(result);
    }

    public Task<Order> FindOneWithoutRelations(int id)
    {
        var result = _context.Orders
            .FirstOrDefault(order => order.Id == id) 
            ?? throw new Exception("Order not found");

        return Task.FromResult(result);
    }
}