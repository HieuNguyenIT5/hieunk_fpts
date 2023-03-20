using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("OrderItem")]
    public class OrderItem : Entity
    {
        [Required]
        public int OrderId{ get; set; }

        [Required]
        public int ProductId{ get; set; }

        [Required]
        public int Units { get; set; } = 1;

        [Required]
        public decimal UnitPrice{ get; set; } = decimal.Zero;

        public virtual Order Order { get; set; }
    }
}
