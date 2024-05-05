using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Product")]
public class Product
{
    [Key, Required]
    public Guid ProductId { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public decimal? Price { get; set; }
    public string Color { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    [Required]
    public int? Quantity { get; set; }
    public DateTime CreateAt { get; set; }
    [Required]
    public Guid CategoriesId { get; set; }
    [ForeignKey("CategoriesId")]
    public virtual Categories? Categories { get; set; }

    /*     public Guid WishListId { get; set; }
        [ForeignKey("WishListId")]
        public virtual WishLis? WishList { get; set; }

        public Guid CartId { get; set; }
        [ForeignKey("CartId")]
        public virtual Cart? Cart { get; set; } */
}
