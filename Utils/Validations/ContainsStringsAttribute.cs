using System.ComponentModel.DataAnnotations;

namespace onboarding_dotnet.Utils.Validations;

public class ContainsStringsAttribute(string[] validStrings) : ValidationAttribute
{
    private readonly string[] _validStrings = validStrings;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {   
        // Check if all elements are strings
        if (value is string strValue)
        {   
            // Check if the string contains any of the valid strings
            foreach (var validString in _validStrings)
            {
                if (strValue.Contains(validString, StringComparison.OrdinalIgnoreCase))
                {
                    return ValidationResult.Success;
                }
            }

            // Get the field name dynamically
            var fieldName = validationContext.DisplayName ?? validationContext.MemberName;

            // Generate a default error message
            var errorMessage = $"{fieldName} must include one of the following: {string.Join(", ", _validStrings)}";

            return new ValidationResult(errorMessage);
        }

        // Return an error if all elements isn't a string
        return new ValidationResult("Invalid data type.");
    }
}
