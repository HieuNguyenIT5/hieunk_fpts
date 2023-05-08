using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Account.Domain.AggregateModels;
[Table("OrderItem")]
public class OrderItem
{
    [JsonIgnore]
    public int OrderId { get; set; }=default(int);
    public int ProductId { get; set; }
    public int Quantity { get; set; } = 0;
    public decimal Price { get; set; } = decimal.Zero;
    [JsonIgnore]
    public virtual Orders Order { get; set; }
    [JsonIgnore]
    public virtual Product Product { get; set; }

    public OrderItem(int OrderId, int ProductId, int Quantity, decimal Price) {
        this.OrderId = OrderId;
        this.ProductId = ProductId;
        this.Quantity = Quantity;
        this.Price = Price;
    }
    public OrderItem()
    {
        
    }

    //chi tinh toan -+ field entity cua no , get/set value nhung field trong this entity
    public decimal SubTotal()
    {
        return this.Quantity * this.Price;
    }
}
