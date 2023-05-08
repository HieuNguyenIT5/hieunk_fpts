using System.ComponentModel.DataAnnotations.Schema;

namespace Account.Domain.AggregateModels;
[Table("Orders")]
public class Orders : Entity
{
    public int OrderId { get; set; }
    public DateTime DateTime { get; set; }
    public string IP { get; set; } = string.Empty;
    public int STATUS { get; set; } = 1;
    public Customer Customer { get; set; }
    public Revenue Revenue { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}
