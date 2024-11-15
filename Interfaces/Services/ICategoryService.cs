using onboarding_dotnet.Dtos.Categories;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Interfaces.Services
{
    public interface ICategoryService : IBaseService<Category, CategoryRequestDto>
    {
    }
}