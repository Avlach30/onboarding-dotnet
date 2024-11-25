using onboarding_dotnet.Providers;
using onboarding_dotnet.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add configuration to the container.
var appServiceProvider = new AppServiceProvider(builder);
appServiceProvider.InitServiceProvider();
appServiceProvider.InitRepositoryInjection();
appServiceProvider.InitServiceInjection();

var app = builder.Build();

var schedulerFactoryProvider = new SchedulerFactoryProvider(app);
await schedulerFactoryProvider.InitSchedulerFactory();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UsePathBase(new PathString("/api"));

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ValidateJsonKeysMiddleware>();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();
