using onboarding_dotnet.Repositories;

namespace onboarding_dotnet.Providers
{
    public partial class AppServiceProvider
    {
        public void InitRepositoryInjection()
        {
            builder.Services.AddScoped<UserRepository, UserRepository>();
            builder.Services.AddScoped<ProductRepository, ProductRepository>();
            builder.Services.AddScoped<CategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<TransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<OrderRepository, OrderRepository>();
        }
    }
}