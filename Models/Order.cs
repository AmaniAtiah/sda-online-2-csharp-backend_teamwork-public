using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    [Table("Order")]
    public class Order
    {
        public Guid OrderId { get; set; }//PK
        /// <summary>
        //public Guid UserId { get; set; }//FK
        /// </summary>
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        [Required(ErrorMessage = "Total Price is required")]
        public decimal? TotalPrice { get; set; }
        [Required(ErrorMessage = "OrderStatus is required")]
        public String? Status { get; set; }
        //Relations:
        [Required(ErrorMessage = "UserId is required")]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid AddresseId { get; set; }
        public Address Addresses { get; set; }
    }
}