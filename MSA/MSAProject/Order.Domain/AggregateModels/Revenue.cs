using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Domain.AggregateModels;
[Table("Revenue")]
public class Revenue
{
    [Key]
    public int RevenueId { get; set; }
    public int OrderId { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }

    public Revenue(int orderId, decimal total) 
    { 
        OrderId = orderId;
        Total = total;
        CreatedAt = DateTime.Now;
    }
    public Revenue()
    {
        
    }
}
