using System.ComponentModel.DataAnnotations;

namespace onboarding_dotnet.Dtos.Users;

public class UserRequestDto
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Name must be less than 100 characters")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Address is required")]
    [MaxLength(100, ErrorMessage = "Address must be less than 100 characters")]
    public required string Address { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    [MaxLength(100, ErrorMessage = "Email must be less than 100 characters")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be greater than 6 characters")]
    [MaxLength(20, ErrorMessage = "Password must be less than 20 characters")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare("Password", ErrorMessage = "Password and Confirm Password must be the same")]
    public required string ConfirmPassword { get; set; }
}