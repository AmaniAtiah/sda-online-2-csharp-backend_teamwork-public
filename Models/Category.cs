using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
  [Table("Category")]
  public class Category
  {
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "Category name is requierd")]
    [MinLength(2, ErrorMessage = "Category name must be at least 2 characters long.")]
    [MaxLength(50, ErrorMessage = "Category name must not exceed 50 characters long.")]
    public required string category_name { get; set; }

    public string Slug { get; set; } = string.Empty;

    [MaxLength(300, ErrorMessage = "Description cannot exceed 300 characters long. Please shorten it and try again..")]
    public string Description { get; set; } = string.Empty;

    //Relations:
    public List<Product> Products { get; set; } = new List<Product>();
  }
}