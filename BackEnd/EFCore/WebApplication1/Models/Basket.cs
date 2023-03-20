namespace WebApplication1.Models
{
    public class Basket
    {
        public int ProductId { get; set; }
        public int BuyerId { get; set; }
        public string ProductName { get; set; }
        public int Units { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
