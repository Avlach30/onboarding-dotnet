using Microsoft.AspNetCore.Mvc;
using onboarding_dotnet.Dtos.Users;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Interfaces.Services;

namespace onboarding_dotnet.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse>> Register([FromBody] UserRequestDto registerRequestDto)
    {
        await _authService.RegisterAsync(registerRequestDto);

        return Ok(ApiResponse.Success("Register success"));
    }
}