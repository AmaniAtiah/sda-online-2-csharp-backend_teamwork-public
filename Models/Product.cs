using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
        [Table("Products")]
        public class Product
        {
                public Guid ProductId { get; set; }
                [Required(ErrorMessage = "Product Name is required")]
                [MaxLength(150, ErrorMessage = "Product Name must be less than 150 character")]
                [MinLength(2, ErrorMessage = "Product Name must be at least 2 character")]
                public string Name { get; set; } = string.Empty;
                public string Description { get; set; } = string.Empty;
                [Required(ErrorMessage = "Price is required")]
                public decimal? Price { get; set; }
                public string Color { get; set; } = string.Empty;
                public string Size { get; set; } = string.Empty;
                public string Brand { get; set; } = string.Empty;
                [Required(ErrorMessage = "Quantity is required")]
                public int? Quantity { get; set; }
                public DateTime CreateAt { get; set; } = DateTime.UtcNow;
                //Relations:
                [Required(ErrorMessage = "CategoryId is required")]
                public Guid CategoriesId { get; set; }
                public Categories? Category { get; set; }
                // [Required(ErrorMessage = "OrderId is required")]
                // public Guid OrderId { get; set; }
                // public Order? Orders { get; set; }
                public List<Order> Orders { get; set; } = new List<Order>();
        }
}