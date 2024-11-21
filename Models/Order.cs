using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding_dotnet.Models;

[Table("orders")]
public class Order: BaseModel
{
    public required string Status { get; set; }


    [Column("Total_Price", TypeName = "decimal(12,2)")]
    public decimal TotalPrice { get; set; }

    [Column("User_Id")]
    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public ICollection<OrderProduct> OrderProducts { get; } = [];

    public Transaction? Transaction { get; set; }
}