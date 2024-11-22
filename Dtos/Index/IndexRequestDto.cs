namespace onboarding_dotnet.Dtos.Index;

public class IndexRequestDto
{
    public string? Search { get; set; }
    public int Page { get; set; } = 1;
    public int PerPage { get; set; } = 10;
    public string? OrderBy { get; set; }
    public string? OrderDirection { get; set; }
}