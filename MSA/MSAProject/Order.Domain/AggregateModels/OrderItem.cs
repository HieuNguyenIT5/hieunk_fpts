using System.ComponentModel.DataAnnotations.Schema; 

namespace Order.Domain.AggregateModels;
[Table("Orders")]
public class OrderItem : Entity
{
    public int OrderId { get; set; }
    public string CustomerId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; } = 0;
    public decimal Price { get; set; } = decimal.Zero;
    public DateTime DateTime { get; set; }
    public string IP { get; set; } = string.Empty;
    //public virtual Product Product { get; set; }= new Product();
    //public virtual Customer Customer { get; set; }= new Customer();
    //public Revenue Revenue { get; set; } = new Revenue();
    public OrderItem(string CustomerId, int ProductId, int Quantity, decimal Price, string IP) {
        this.OrderId = 0;
        this.CustomerId = CustomerId;
        this.ProductId = ProductId;
        this.Quantity = Quantity;
        this.Price = Price;
        this.IP = IP;
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
