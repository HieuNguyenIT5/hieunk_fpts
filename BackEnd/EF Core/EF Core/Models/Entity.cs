using System.ComponentModel.DataAnnotations;

namespace EF_Core.Models
{
    public class Entity
    {
        [Key]
        public int id{ get; set; }
    }
}
