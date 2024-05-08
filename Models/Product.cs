using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
        [Table("Product")]
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
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// public class Product
// {
//     public Guid ProductId { get; set; }
//     [Required(ErrorMessage = "Product name is requierd")]
//     [MinLength(2, ErrorMessage = "Product name must be at least 2 characters long.")]
//     [MaxLength(150, ErrorMessage = "Product name must not exceed 150 characters long.")]
//     public string Name { get; set; } = string.Empty;
//     [Required(ErrorMessage = "Product description is requierd")]
//     public string Description { get; set; } = string.Empty;
//     [Required(ErrorMessage = "Product price is requierd")]
//     public decimal? Price { get; set; }
//     public string Color { get; set; } = string.Empty;
//     public string Size { get; set; } = string.Empty;
//     public string Brand { get; set; } = string.Empty;
//     [Required(ErrorMessage = "Product quantity is requierd")]
//     public int? Quantity { get; set; }
//     //ypublic DateTime CreateAt { get; set; }
//     //public Guid CategoriesId { get; set; }
//     //[ForeignKey("CategoriesId")]
//     // public virtual Categories? Categories { get; set; }

//     /*     public Guid WishListId { get; set; }
//         [ForeignKey("WishListId")]
//         public virtual WishLis? WishList { get; set; }

//         public Guid CartId { get; set; }
//         [ForeignKey("CartId")]
//         public virtual Cart? Cart { get; set; } */
// }
