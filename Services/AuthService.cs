using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using onboarding_dotnet.Dtos.Auth;
using onboarding_dotnet.Dtos.Users;
using onboarding_dotnet.Interfaces.Repositories;
using onboarding_dotnet.Interfaces.Services;
using onboarding_dotnet.Mappers;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Services;

public class AuthService(IUserRepository userRepository, IConfiguration configuration): IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IConfiguration _config = configuration;

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

    public async Task<AuthLoginResponseDto> LoginAsync(AuthLoginRequestDto data)
    {
        var user = await _userRepository.FindByEmailAsync(data.Email) ?? throw new Exception("User not found");

        // Check if the password is correct
        if (!BCrypt.Net.BCrypt.Verify(data.Password, user.Password))
        {
            throw new Exception("Invalid password");
        }

        var token = GenerateToken(user);

        return new AuthLoginResponseDto
        {
            User = user.ToResponse(),
            Token = token
        };
    }

    private string GenerateToken(User user)
    {   
        // Get the secret key from the configuration
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]!));
        // Create the credentials
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        // Create the claims for the user
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name!),
            new Claim(ClaimTypes.Email, user.Email!),
        };

        // Create the token
        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            userClaims,
            expires: DateTime.Now.AddDays(_config.GetValue<int>("Jwt:ExpirationTimeInDays")),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}