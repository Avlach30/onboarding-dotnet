namespace onboarding_dotnet.Interfaces.Services
{
    public interface IUserService<TData>
    {
        Task<TData> GetOneByEmail(string email);

        Task<TData> GetOneById(int id);
    }
    
}