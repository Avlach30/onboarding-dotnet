namespace onboarding_dotnet.Infrastructures.Policies;

using System.Text;
using System.Text.Json;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        // Allocate a string builder with the original size of the name
        var builder = new StringBuilder(name.Length + 10);

        // Iterate through all the characters in the name
        for (var i = 0; i < name.Length; i++)
        {
            var c = name[i];

            // If the character is uppercase and not the first character
            if (char.IsUpper(c) && i > 0)
            {
                // Add an underscore before the uppercase character
                builder.Append('_');
            }

            // Add the lowercase version of the character to the builder
            builder.Append(char.ToLower(c));
        }

        // Return the built string
        return builder.ToString();
    }
}