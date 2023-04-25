namespace Account.Domain.AggregateModels;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName{ get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;
    public int QuantitySold { get; set; } = 0;
    public DateTime CreatedAt { get; set; }
    public List<Order> Orders { get; set; }=new List<Order>();
}
