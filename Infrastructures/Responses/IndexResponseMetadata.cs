namespace onboarding_dotnet.Infrastructures.Responses;

public class IndexResponseMetadata
{
    public int Total { get; set; }
    public int Page { get; set; } = 1;
    public int PerPage { get; set; } = 10;

    public IndexResponseMetadata(int total, int page, int perPage) => (Total, Page, PerPage) = (total, page, perPage);
}