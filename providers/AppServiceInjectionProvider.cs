using onboarding_dotnet.Services;

namespace onboarding_dotnet.Providers
{
    public partial class AppServiceProvider
    {
        public void InitServiceInjection()
        {
            builder.Services.AddScoped<CategoryIndexService, CategoryIndexService>();
            builder.Services.AddScoped<CategoryService, CategoryService>();
            builder.Services.AddScoped<ProductIndexService, ProductIndexService>();
            builder.Services.AddScoped<ProductService, ProductService>();
            builder.Services.AddScoped<OrderIndexService, OrderIndexService>();
            builder.Services.AddScoped<OrderService, OrderService>();
            builder.Services.AddScoped<TransactionService, TransactionService>();
            builder.Services.AddScoped<UserService, UserService>();
            builder.Services.AddScoped<AuthService, AuthService>();
        }
    }
}