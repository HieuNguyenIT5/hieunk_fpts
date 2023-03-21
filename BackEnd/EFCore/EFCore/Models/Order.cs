using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;

namespace WebApplication1.Models;

[Table("Order")]
public class Order : Entity
{
    public DateTime CreateDate { get; set; } = DateTime.Now;

    public int Status{ get; set; }

    public int BuyerId{ get; set; }

    public string address{ get; set; }
    public virtual List<OrderItem>? items { get; set; }
}