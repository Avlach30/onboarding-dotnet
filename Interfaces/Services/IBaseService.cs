using System.Runtime.CompilerServices;

namespace onboarding_dotnet.Interfaces.Services

{
    public interface IBaseService<TData, TRequest>
    {
        Task<List<TData>> GetAll();
        Task<TData> GetOne(int id);
        Task<AsyncVoidMethodBuilder> Create(TRequest data);
        Task<AsyncVoidMethodBuilder> Update(int id, TRequest data);
        Task<bool> Delete(int id);
    }
}