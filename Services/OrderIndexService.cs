using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Dtos.Index;
using onboarding_dotnet.Dtos.Orders;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Mappers;
using onboarding_dotnet.Utils.Helpers;

namespace onboarding_dotnet.Services;

public class OrderIndexService(
    ApplicationDBContext context
)
{
    private readonly ApplicationDBContext _context = context;

    public async Task<IndexResponse<OrderDto>> Fetch(IndexRequestDto request)
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
}