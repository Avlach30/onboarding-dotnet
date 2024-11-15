using onboarding_dotnet.Dtos.Users;
using onboarding_dotnet.Models;
using onboarding_dotnet.Utils.Helpers;

namespace onboarding_dotnet.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Address = user.Address,
            Email = user.Email,
            Password = user.Password,
            CreatedAt = user.Created_at,
            UpdatedAt = user.Updated_at
        };
    }

    public static UserResponseDto ToResponse(this User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            RegisteredAt = user.Created_at
        };
    }

    public static User ToModel(this UserRequestDto userRequestDto)
    {
        var password = PasswordHelper.Encrypt(userRequestDto.Password);

        return new User
        {
            Name = userRequestDto.Name,
            Address = userRequestDto.Address,
            Email = userRequestDto.Email,
            Password = password
        };
    }
}