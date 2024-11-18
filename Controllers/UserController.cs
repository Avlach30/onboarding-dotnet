using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using onboarding_dotnet.Dtos.Users;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Interfaces.Repositories;
using onboarding_dotnet.Interfaces.Services;
using onboarding_dotnet.Mappers;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Controllers;

[ApiController]
[Route("users")]
public class UserController(IUserService userService): Controller
{
    private readonly IUserService _userService = userService;

    [HttpGet("profile")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<UserResponseDto>>> Profile()
    {   
        // Get value of the user id from the token claims
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        // Find the user by the user id
        var user = await _userService.GetOneById(userId);

        return Ok(ApiResponse<UserResponseDto>.Success(user.ToResponse(), "Get profile success"));
    }
};