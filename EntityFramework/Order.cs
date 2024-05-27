using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.EntityFramework
{
        [Table("Order")]
        public class Order
        {
         public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public List<OrderProduct>? OrderProducts { get; set; }
        }
}