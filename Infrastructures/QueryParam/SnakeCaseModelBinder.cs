namespace onboarding_dotnet.Infrastructures.QueryParam;

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Collections.Generic;
using onboarding_dotnet.Utils.Helpers;
using System.Text.RegularExpressions;

// This class is used to bind the query parameters to the model with snake_case naming convention
public class SnakeCaseQueryModelBinder : IModelBinder
{
    private static readonly Regex _snakeCasePattern = new Regex(@"^[a-z0-9_]+$", RegexOptions.Compiled);

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        // Get the query parameters
        var queryCollection = bindingContext.HttpContext.Request.Query;
        var convertedValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var kvp in queryCollection)
        {
            // Check if the query parameter is in snake_case format
            if (!_snakeCasePattern.IsMatch(kvp.Key))
            {
                bindingContext.ModelState.AddModelError(kvp.Key, $"The query parameter '{kvp.Key}' must be in snake_case.");
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            var pascalCaseKey = NamingConventionConverterHelper.SnakeCaseToPascalCase(kvp.Key);
            convertedValues[pascalCaseKey] = kvp.Value;
        }

        // Create the model instance and set the properties
        var model = Activator.CreateInstance(bindingContext.ModelType);

        foreach (var property in bindingContext.ModelType.GetProperties())
        {
            if (convertedValues.TryGetValue(property.Name, out var value))
            {
                property.SetValue(model, Convert.ChangeType(value, property.PropertyType));
            }
        }

        bindingContext.Result = ModelBindingResult.Success(model);
        return Task.CompletedTask;
    }
}
