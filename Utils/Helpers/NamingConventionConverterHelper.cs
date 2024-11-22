namespace onboarding_dotnet.Utils.Helpers;

public static class NamingConventionConverterHelper
{
    public static string SnakeCaseToPascalCase(string snakeCase)
    {
        return string.Join("", snakeCase
            .Split('_')
            .Select(word => char.ToUpper(word[0]) + word.Substring(1)));
    }
}
