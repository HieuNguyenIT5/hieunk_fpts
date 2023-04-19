namespace Account.Domain.AggregateModels;
public class Order : Entity
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; } = 0;
    public decimal Price { get; set; } = decimal.Zero;
    public DateTime DateTime { get; set; }
    public string IP { get; set; } = string.Empty;
    public int STATUS { get; set; }

    public decimal SubTotal()
    {
        return this.Quantity * this.Price;
    }
}
