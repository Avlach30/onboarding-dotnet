using onboarding_dotnet.Dtos.Users;

namespace onboarding_dotnet.Dtos.Auth;

public class AuthLoginResponseDto
{
    public required UserResponseDto User { get; set; }
    public required string Token { get; set; }
}