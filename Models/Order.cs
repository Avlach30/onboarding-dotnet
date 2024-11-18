using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using onboarding_dotnet.Utils.Enums;

namespace onboarding_dotnet.Models;

public class Order: BaseModel
{
    [DefaultValue(OrderStatus.Draft)]
    public required string Status { get; set; }


    [Column("Total_Price", TypeName = "decimal(12,2)")]
    public decimal TotalPrice { get; set; }

    [Column("User_Id")]
    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public ICollection<OrderProduct> OrderProducts { get; } = [];
}