namespace onboarding_dotnet.Utils.Helpers;

public class PasswordHelper
{
    public static string Encrypt(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}