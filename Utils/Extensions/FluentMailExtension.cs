namespace onboarding_dotnet.Utils.Extensions;

public static class FluentMailExtension
{
    public static void AddFluentEMail(this IServiceCollection services, IConfiguration configuration)
    {
        var emailConfig = configuration.GetSection("Email");
        
        var defaultFromEmail = emailConfig["DefaultFromEmail"];
        var defaultFromName = emailConfig["DefaultFromName"];
        var host = emailConfig["Host"];
        var port = emailConfig.GetValue<int>("Port");
        var username = emailConfig["Username"];
        var password = emailConfig["Password"];

        services.AddFluentEmail(defaultFromEmail, defaultFromName)
            .AddMailtrapSender(username, password, host, port)
            .AddRazorRenderer();
    }
}