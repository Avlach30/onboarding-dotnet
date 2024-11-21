using onboarding_dotnet.Interfaces.Repositories;
using onboarding_dotnet.Repositories;

namespace onboarding_dotnet.Providers
{
    public partial class AppServiceProvider
    {
        public void InitRepositoryInjection()
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}