using Backend.Models;
namespace Backend.Dtos
{
    public class CartDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Guid User { get; set; }
        public Product? Product { get; set; }
    }
}
