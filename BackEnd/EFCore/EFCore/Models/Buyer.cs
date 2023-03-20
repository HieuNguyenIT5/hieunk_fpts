using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Core.Models
{
    [Table("Buyer")]
    public class Buyer : Entity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string PaymmentMethod  { get; set; } = string.Empty;
    }
}
