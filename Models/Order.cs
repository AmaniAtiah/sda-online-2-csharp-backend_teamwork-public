using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    [Table("Orders")]
    public class Order
    {
        public Guid OrderId { get; set; }//PK
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        [Required(ErrorMessage = "Total Price is required")]
        public decimal? TotalPrice { get; set; }
        [Required(ErrorMessage = "OrderStatus is required")]
        public String? Status { get; set; }
        //Relations:
        [Required(ErrorMessage = "UserId is required")]
        public Guid UserId { get; set; }//FK
        public User? User { get; set; }
        [Required(ErrorMessage = "AddresseId is required")]
        // public Guid AddresseId { get; set; }
        // public Address? Addresses { get; set; }//FK
        public List<Product> Products { get; set; } = new List<Product>();
    }
}