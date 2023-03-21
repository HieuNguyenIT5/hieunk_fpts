using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;

namespace WebApplication1.Models;

[Table("OrderItem")]
public class OrderItem
{
    public int OrderId{ get; set; }

    public int ProductId{ get; set; }

    public int Units { get; set; } = 1;

    public decimal UnitPrice{ get; set; } = decimal.Zero;
}
