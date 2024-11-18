using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding_dotnet.Models;

[Table("transactions")]
public class Transaction: BaseModel
{   
    [Column("payment_method")]
    public string PaymentMethod { get; set; } = null!;

    [Column("payment_status")]
    public string PaymentStatus { get; set; } = null!;

    [Column("order_id")]
    public int OrderId { get; set; }

    public Order Order { get; set; } = null!;
}