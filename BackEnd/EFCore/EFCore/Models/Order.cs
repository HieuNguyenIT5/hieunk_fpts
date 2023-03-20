using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Core.Models
{
    [Table("Order")]
    public class Order : Entity
    {
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Required]
        public int Status{ get; set; } = 0;

        [Required]
        public int BuyerId{ get; set; }

        [Required]
        public string address{ get; set; } = string.Empty;
    }
}
