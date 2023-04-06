using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Domain.AggregateModels;

[Table("Customers")]
public class Customer : Entity
{
    public string CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal CustomerWallet { get; set; } = decimal.Zero;
    public DateTime CreatedAt { get; set; }
    public string CustomerMail { get; set; }

    public bool checkCashCustomer(decimal totalCart)
    {
        if(this.CustomerWallet > totalCart) 
        {
            return true;
        }
        return false;
    }
}
