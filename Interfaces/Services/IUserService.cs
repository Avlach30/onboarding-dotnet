using onboarding_dotnet.Models;

namespace onboarding_dotnet.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> GetOneByEmail(string email);

        Task<User> GetOneById(int id);
    }
    
}