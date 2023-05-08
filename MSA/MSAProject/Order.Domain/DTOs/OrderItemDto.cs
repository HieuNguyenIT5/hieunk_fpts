namespace Order.Domain.DTOs;

public class OrderItemDto
{
    public int OrderId { get; set; } = default(int);
    public int ProductId { get; set; }
    public int Quantity { get; set; } = 0;
    public decimal Price { get; set; } = decimal.Zero;
}
