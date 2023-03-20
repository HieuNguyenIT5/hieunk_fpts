using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    [Table("Order")]
    public class Order : Entity
    {
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Required]
        public int Status{ get; set; }

        [Required]
        public int BuyerId{ get; set; }

        [Required]
        public string address{ get; set; }

        public virtual Buyer Buyer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
