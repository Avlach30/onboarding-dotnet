using System.Runtime.CompilerServices;
using onboarding_dotnet.Dtos.Auth;
using onboarding_dotnet.Dtos.Users;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<AsyncVoidMethodBuilder> RegisterAsync(UserRequestDto userRequestDto);
        public Task<AuthLoginResponseDto> LoginAsync(AuthLoginRequestDto authLoginRequestDto);
    }
}