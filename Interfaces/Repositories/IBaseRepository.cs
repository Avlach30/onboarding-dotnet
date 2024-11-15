using System.Runtime.CompilerServices;

namespace onboarding_dotnet.Interfaces.Repositories

{
    public interface IBaseRepository<TData>
    {
        Task<List<TData>> FindAll();
        Task<TData> FindOne(int id);
        Task<AsyncVoidMethodBuilder> CreateAsync(TData entity);
        Task<AsyncVoidMethodBuilder> UpdateAsync(TData entity);
        Task<bool> Delete(int id);
    }
}