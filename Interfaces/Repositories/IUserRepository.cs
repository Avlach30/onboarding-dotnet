using System.Runtime.CompilerServices;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> FindByEmailAsync(string email);

        public Task<User?> FindByIdAsync(int id);

        public Task<AsyncVoidMethodBuilder> CreateAsync(User user);
    }
}