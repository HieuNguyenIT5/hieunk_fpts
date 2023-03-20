using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Buyer")]
    public class Buyer : Entity
    {
        public Buyer()
        {
            Orders = new HashSet<Order>();
        }
        public virtual ICollection<Order> Orders { get; set; }

        public string Name { get; set; }

        public string PaymentMethod { get; set; }
    }
}
