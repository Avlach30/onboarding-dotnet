using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Infrastructures.Mails.Interfaces;
using onboarding_dotnet.Infrastructures.Mails.Services;
using onboarding_dotnet.Infrastructures.Policies;
using onboarding_dotnet.Infrastructures.QueryParam;
using onboarding_dotnet.Infrastructures.Schedulers;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Utils.Extensions;
using Quartz;

namespace onboarding_dotnet.Providers
{
    public partial class AppServiceProvider(WebApplicationBuilder _builder)
    {
        private readonly WebApplicationBuilder builder = _builder;

        public void InitServiceProvider()
        {
                        // Add configuration to the container.
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            // Add Quartz for scheduling jobs
            builder.Services.AddQuartz();
            builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            builder.Services.AddSingleton<IJob, OrderAutoCancelJob>();

            // Add JWT authentication middleware
            builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
                    };
                }
            });

            builder.Services.AddControllers(options => {
                // Register the snake_case query model binder
                options.ModelBinderProviders.Insert(0, new SnakeCaseQueryBinderProvider());
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
                options.JsonSerializerOptions.DictionaryKeyPolicy = new SnakeCaseNamingPolicy();
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure Logger
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            builder.Logging.SetMinimumLevel(LogLevel.Information);
            builder.Logging.AddFilter("Microsoft", LogLevel.Warning);
            builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning);
            builder.Logging.AddFilter("System", LogLevel.Warning);
            builder.Logging.AddFilter("onboarding_dotnet", LogLevel.Trace);

            builder.Services.AddDbContextPool<ApplicationDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddFluentEMail(builder.Configuration);
            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddHttpContextAccessor();
        }
    }
}