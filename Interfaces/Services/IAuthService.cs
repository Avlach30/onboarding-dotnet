using System.Runtime.CompilerServices;
using onboarding_dotnet.Dtos.Users;

namespace onboarding_dotnet.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<AsyncVoidMethodBuilder> RegisterAsync(UserRequestDto userRequestDto);
    }
}