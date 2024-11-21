using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding_dotnet.Models;

[Table("orders")]
public class Order: BaseModel
{   
    [Column("status")]
    public required string Status { get; set; }


    [Column("total_price", TypeName = "decimal(12,2)")]
    public decimal TotalPrice { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public ICollection<OrderProduct> OrderProducts { get; } = [];

    public Transaction? Transaction { get; set; }
}