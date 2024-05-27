

using Backend.EntityFramework;

namespace Backend.Dtos
{
    public class CartProductDto
    {
        public Guid CartId { get; set; }
        public Cart? Cart { get; set; }
        public Guid ProductId { get; set; }
        public ProductDtos? Product { get; set; }
        public int Quantity { get; set; }
        // date
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}