using System.ComponentModel.DataAnnotations;

namespace onboarding_dotnet.Dtos.Auth;

public class AuthLoginRequestDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; set; }

}