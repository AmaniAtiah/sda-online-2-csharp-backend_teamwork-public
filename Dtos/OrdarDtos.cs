using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Backend.EntityFramework;

namespace Backend.Dtos
{
    public class OrderDtos
    {
        public Guid OrderId { get; set; }//PK
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        [Required(ErrorMessage = "Total Price is required")]
        public decimal? TotalPrice { get; set; }
        public String? Status { get; set; }
        public Guid UserId { get; set; }
    }
}