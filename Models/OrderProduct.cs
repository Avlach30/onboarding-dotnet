using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding_dotnet.Models;

[Table("order_products")]
public class OrderProduct: BaseModel
{
    [Column("Order_Id")]
    public int OrderId { get; set; }

    public Order Order { get; set; } = null!;

    [Column("Product_Id")]
    public int ProductId { get; set; }

    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }
}