using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Core.Models
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
    }
}
