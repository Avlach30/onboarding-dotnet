namespace onboarding_dotnet.Infrastructures.Repositories;

public class PaginationResult<T>
{
    public List<T> Data { get; set; } = null!;
    public int Total { get; set; }
}