namespace onboarding_dotnet.Utils.Helpers;

public class PaginationHelper
{
    public static int GetSkip(int page, int perPage) => (page - 1) * perPage;
}