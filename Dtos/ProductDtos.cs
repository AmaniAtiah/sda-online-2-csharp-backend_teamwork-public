
using Backend.EntityFramework;

namespace Backend.Dtos
{
    public class ProductDtos
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Slug { get; set; } 
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;

        public decimal? Price { get; set; }
        public int? Quantity { get; set; } 
        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public Guid? CategoriesId { get; set; }
        public Category? Category { get; set; }
        // list of orders
        
        // cartproduct  ICollection
        // public ICollection<CartProductDto> CartProducts { get; set; } = new List<CartProductDto>();
       public List<CartProductDto>? CartProducts { get; set; }  = new List<CartProductDto>();
        public List<OrderProductDto>? OrderProducts { get; set; } = new List<OrderProductDto>();



        // cartproduct






    }
}