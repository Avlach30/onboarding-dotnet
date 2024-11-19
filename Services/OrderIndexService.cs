using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Dtos.Index;
using onboarding_dotnet.Dtos.Orders;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Interfaces.Services.Indexes;
using onboarding_dotnet.Mappers;
using onboarding_dotnet.Utils.Helpers;

namespace onboarding_dotnet.Services;

public class OrderIndexService(
    ApplicationDBContext context
): IOrderIndexService
{
    private readonly ApplicationDBContext _context = context;

    public async Task<IndexResponse<OrderResponseDto>> Fetch(IndexRequestDto request)
    {
        var datas = _context.Orders.Include(order => order.Transaction).AsSplitQuery().AsQueryable();

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

        return IndexResponse<OrderResponseDto>.Success(
            result.Select(order => order.ToResponse()).ToList(),
            totalData,
            "Get orders success", 
            request.Page, 
            request.PerPage
        );
    }
}