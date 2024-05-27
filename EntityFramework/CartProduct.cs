using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.EntityFramework
{
    [Table("CartProduct")]
    public class CartProduct
    {
        
        // [ForeignKey("CartId")]
        public Guid CartId { get; set; }
        public Cart? Cart { get; set; }
        // [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; } = 1;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        



    }
}