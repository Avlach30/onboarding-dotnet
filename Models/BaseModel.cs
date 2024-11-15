namespace onboarding_dotnet.Models;

public class BaseModel
{
    public int Id { get; set; }
    public DateTime Created_at { get; set; } = DateTime.Now;
    public DateTime Updated_at { get; set; } = DateTime.Now;
    public DateTime? Deleted_at { get; set; }
}