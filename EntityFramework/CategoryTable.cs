using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Models;

namespace Backend.EntityFramework
{
  [Table("Categories")]
  public class CategoryTable
  {
    [Key] 
    [Column("category_id")]
    public Guid category_id { get; set; }

    [Required]
    [MinLength(2)]
    [Column("category_name")]
    public string? category_name { get; set; }

    [Column("slug")]
    public string Slug { get; set; } = string.Empty;

    [Column("description")]
    public string description { get; set; } = string.Empty;

    public ICollection<Product> Products { get; } = new List<Product>();
  }
}