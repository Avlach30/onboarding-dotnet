namespace onboarding_dotnet.Utils.Helpers;

public static class GetBaseUrl
{
    public static string Get(IHttpContextAccessor httpContextAccessor)
    {
        var request = httpContextAccessor.HttpContext.Request;
        return $"{request.Scheme}://{request.Host}";
    }
}