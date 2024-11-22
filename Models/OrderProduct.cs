using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding_dotnet.Models;

[Table("order_products")]
public class OrderProduct: BaseModel
{
    [Column("order_id")]
    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    [Column("product_id")]
    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    [Column("quantity")]
    public int Quantity { get; set; }
}