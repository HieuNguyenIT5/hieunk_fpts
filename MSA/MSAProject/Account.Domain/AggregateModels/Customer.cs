namespace Account.Domain.AggregateModels;

public class Customer : Entity
{
    public string CustomerName    { get; set; } = string.Empty;
    public decimal CustomerWallet { get; set; } = decimal.Zero;
    public DateTime CreatedAt     { get; set; }
    public List<Order> Orders     { get; set; } = new List<Order>();

    public bool checkCashCustomer(decimal totalCart)
    {
        if(this.CustomerWallet > totalCart) 
        {
            return true;
        }
        return false;
    }

    public decimal getCustomerWallet()
    {
        return this.CustomerWallet;
    }
}
