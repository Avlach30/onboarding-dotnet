using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Dtos.Categories;
using onboarding_dotnet.Dtos.Index;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Interfaces.Services.Indexes;
using onboarding_dotnet.Mappers;
using onboarding_dotnet.Utils.Helpers;

namespace onboarding_dotnet.Services;

public class CategoryIndexService(
    ApplicationDBContext context
): ICategoryIndexService
{
    private readonly ApplicationDBContext _context = context;

    public async Task<IndexResponse<CategoryDto>> Fetch(IndexRequestDto request)
    {
        var datas = _context.Categories.AsQueryable();

        // Default order by created_at desc
        if (string.IsNullOrEmpty(request.OrderBy))
        {
            datas = datas.OrderByDescending(category => category.Created_at);
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

        return IndexResponse<CategoryDto>.Success(
            result.Select(category => category.ToDto()).ToList(),
            totalData,
            "Get categories success", 
            request.Page, 
            request.PerPage
        );
    }
}