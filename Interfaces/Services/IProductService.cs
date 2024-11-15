using onboarding_dotnet.Dtos.Products;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Interfaces.Services
{
    public interface IProductService : IBaseService<Product, ProductRequestDto>
    {
    }
}