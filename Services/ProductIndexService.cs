using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Dtos.Index;
using onboarding_dotnet.Dtos.Products;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Interfaces.Services.Indexes;
using onboarding_dotnet.Mappers;
using onboarding_dotnet.Utils.Helpers;

namespace onboarding_dotnet.Services;

public class ProductIndexService(
    ApplicationDBContext context
)
: IProductIndexService
{
    private readonly ApplicationDBContext _context = context;

    public async Task<IndexResponse<ProductResponseDto>> Fetch(IndexRequestDto request)
    {
        var datas = _context.Products.Include(product => product.Category).AsSplitQuery().AsQueryable();

        // Default order by created_at desc
        if (string.IsNullOrEmpty(request.OrderBy))
        {
            datas = datas.OrderByDescending(product => product.Created_at);
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

        return IndexResponse<ProductResponseDto>.Success(
            result.Select(product => product.ToResponse()).ToList(),
            totalData,
            "Get products success", 
            request.Page, 
            request.PerPage
        );
    }
}