using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding_dotnet.Models;

public class BaseModel
{
    [Column("id")]
    public int Id { get; set; }

    [Column("created_at")]
    public DateTime Created_at { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime Updated_at { get; set; } = DateTime.Now;

    [Column("deleted_at")]
    public DateTime? Deleted_at { get; set; }
}