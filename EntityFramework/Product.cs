using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.EntityFramework
{
        [Table("Products")]
        public class Product
        {
                public Guid ProductId { get; set; }
                [Required(ErrorMessage = "Product Name is required")]
                [MaxLength(150, ErrorMessage = "Product Name must be less than 150 character")]
                [MinLength(2, ErrorMessage = "Product Name must be at least 2 character")]
                public string Name { get; set; } = string.Empty;
                public string Slug { get; set; } = string.Empty;

                public string Description { get; set; } = string.Empty;
                [Required(ErrorMessage = "Price is required")]
                public decimal Price { get; set; }
                // image 
                public string Image { get; set; } = string.Empty;

                public string Color { get; set; } = string.Empty;
                public string Size { get; set; } = string.Empty;
                public string Brand { get; set; } = string.Empty;
                [Required(ErrorMessage = "Quantity is required")]
                public int? Quantity { get; set; }
                public DateTime CreateAt { get; set; } = DateTime.UtcNow;
                //Relations:
                [Required(ErrorMessage = "CategoryId is required")]
                public Guid? CategoriesId { get; set; }
                public Category? Category { get; set; }
                // public List<Order> Orders { get; set; } = new List<Order>();

                // OrderProduct
                public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
                public List<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

        }
}