using onboarding_dotnet.Interfaces.Repositories;
using onboarding_dotnet.Interfaces.Services;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Services;

public class UserService(IUserRepository userRepository): IUserService<User>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<User> GetOneByEmail(string email)
    {
        return await _userRepository.FindByEmailAsync(email) ?? throw new Exception("User not found");
    }

    public async Task<User> GetOneById(int id)
    {
        return await _userRepository.FindByIdAsync(id) ?? throw new Exception("User not found");
    }
}