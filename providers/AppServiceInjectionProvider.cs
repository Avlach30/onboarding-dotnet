using onboarding_dotnet.Interfaces.Services;
using onboarding_dotnet.Interfaces.Services.Indexes;
using onboarding_dotnet.Services;

namespace onboarding_dotnet.Providers
{
    public partial class AppServiceProvider
    {
        public void InitServiceInjection()
        {
            builder.Services.AddScoped<ICategoryIndexService, CategoryIndexService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IProductIndexService, ProductIndexService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IOrderIndexService, OrderIndexService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
        }
    }
}