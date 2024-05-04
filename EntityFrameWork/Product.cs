using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.EntityFrameWork
{
    [Table("Product")]
    public class Product
    {
        [Key, Required]
        public Guid ProductsId { get; set; }
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
        //Adding categories Id
    }
}