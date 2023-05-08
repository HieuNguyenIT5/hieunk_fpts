using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Order.Domain.AggregateModels;
[Table("Orders")]
public class Orders : Entity
{
    public int OrderId { get; set; }
    public DateTime DateTime { get; set; }
    public string IP { get; set; }
    public int STATUS { get; set; } = 1;
    public virtual Customer Customer { get; set; }
    public virtual ICollection<OrderItem> Items { get; set; }

    public Orders(string CustomerId, string IP)
    {
        this.CustomerId = CustomerId;
        this.STATUS = 1;
        this.DateTime = DateTime.Now;
        this.IP = IP;
    }
    public Orders()
    {
        
    }
}
