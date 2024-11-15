namespace onboarding_dotnet.Dtos.Users;

public class UserResponseDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public DateTime RegisteredAt { get; set; }
}