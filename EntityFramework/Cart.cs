using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.EntityFramework
{
    [Table("Carts")]
    public class Cart
    {
        [Key]
        public Guid CartId { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
       public List<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    }
}