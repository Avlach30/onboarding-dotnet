using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Repositories;

public class OrderRepository(ApplicationDBContext context)
{
    private readonly ApplicationDBContext _context = context;

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