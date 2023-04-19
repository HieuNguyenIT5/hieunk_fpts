using System.ComponentModel.DataAnnotations.Schema;
namespace Order.Domain.AggregateModels;
public class Product
{
    public int ProductId { get; set; }
    public string ProductName{ get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;
    public int QuantitySold { get; set; } = 0;
    public DateTime CreatedAt { get; set; }

    public bool checkQuantity(int qty)
    {
        return this.Quantity >= qty ? true : false;
    }
    public void minusQuantity(int number)
    {
        this.Quantity -= number;
    }
    public void plusQuantitySold(int number) 
    {
        this.QuantitySold += number;
    }
}
