namespace Account.Domain.AggregateModels;
public class Revenue
{
    public int RevenueId { get; set; }
    public int OrderId { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
    public Orders Order { get; set; }
}
