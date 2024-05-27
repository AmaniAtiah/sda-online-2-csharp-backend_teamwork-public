

using Backend.EntityFramework;

namespace Backend.Dtos
{
    public class CreateCartProductDto
    {
                public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid CartId { get; set; }
        public Cart? Cart { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; } = 1;

    }
}