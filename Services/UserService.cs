using onboarding_dotnet.Models;
using onboarding_dotnet.Repositories;

namespace onboarding_dotnet.Services;

public class UserService(UserRepository userRepository)
{
    private readonly UserRepository _userRepository = userRepository;

    public async Task<User> GetOneByEmail(string email)
    {
        return await _userRepository.FindByEmailAsync(email) ?? throw new Exception("User not found");
    }

    public async Task<User> GetOneById(int id)
    {
        return await _userRepository.FindByIdAsync(id) ?? throw new Exception("User not found");
    }
}