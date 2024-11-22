using Microsoft.AspNetCore.Mvc;
using onboarding_dotnet.Dtos.Auth;
using onboarding_dotnet.Dtos.Users;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Services;

namespace onboarding_dotnet.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(AuthService authService) : Controller
{
    private readonly AuthService _authService = authService;

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse>> Register([FromBody] UserRequestDto registerRequestDto)
    {
        await _authService.RegisterAsync(registerRequestDto);

        return Ok(ApiResponse.Success("Register success"));
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<AuthLoginResponseDto>>> Login([FromBody] AuthLoginRequestDto loginRequestDto)
    {
        var response = await _authService.LoginAsync(loginRequestDto);

        return Ok(ApiResponse<AuthLoginResponseDto>.Success(response, "Login success"));
    }
}