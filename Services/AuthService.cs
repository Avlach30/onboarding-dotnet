using System.Runtime.CompilerServices;
using onboarding_dotnet.Dtos.Users;
using onboarding_dotnet.Interfaces.Repositories;
using onboarding_dotnet.Interfaces.Services;
using onboarding_dotnet.Mappers;

namespace onboarding_dotnet.Services;

public class AuthService(IUserRepository userRepository): IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<AsyncVoidMethodBuilder> RegisterAsync(UserRequestDto data)
    {
        // Check if the user already exists by email
        var user = await _userRepository.FindByEmailAsync(data.Email);
        if (user != null)
        {
            throw new Exception("User already exists");
        }

        await _userRepository.CreateAsync(data.ToModel());

        return AsyncVoidMethodBuilder.Create();
    }
}