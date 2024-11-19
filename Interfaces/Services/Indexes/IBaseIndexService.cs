using onboarding_dotnet.Dtos.Index;
using onboarding_dotnet.Infrastructures.Responses;

namespace onboarding_dotnet.Interfaces.Services.Indexes
{
    public interface IBaseIndexService<TData>
    {
        Task<IndexResponse<TData>> Fetch(IndexRequestDto request);
    }
}