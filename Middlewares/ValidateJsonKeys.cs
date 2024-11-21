namespace onboarding_dotnet.Middlewares;

using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class ValidateJsonKeysMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        // Only validate JSON requests
        if (context.Request.ContentType == "application/json" &&
            context.Request.Method == HttpMethods.Post)
        {
            // Enable request buffering
            context.Request.EnableBuffering();

            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();

            // Reset the stream position so downstream middleware can read it again
            context.Request.Body.Position = 0;

            // Validate keys are in snake_case
            var isValid = ValidateSnakeCaseKeys(body);
            if (!isValid)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid JSON keys: must use snake_case.");
                return;
            }
        }

        // Continue to the next middleware
        await _next(context);
    }

    private bool ValidateSnakeCaseKeys(string json)
    {
        try
        {
            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            // Recursively check each property
            return ValidateProperties(root);
        }
        catch
        {
            // If JSON parsing fails, assume it's invalid
            return false;
        }
    }

    private bool ValidateProperties(JsonElement element)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            foreach (var property in element.EnumerateObject())
            {
                // Check if the property name is in snake_case
                if (!IsSnakeCase(property.Name))
                {
                    return false;
                }

                // Recursively validate nested properties
                if (!ValidateProperties(property.Value))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool IsSnakeCase(string name)
    {
        // Regex to match snake_case format
        return Regex.IsMatch(name, "^[a-z]+(_[a-z]+)*$");
    }
}
