using System.ComponentModel.DataAnnotations;

public class Categories
{
  public Guid CategoriesId { get; set; }

  [Required(ErrorMessage = "Category name is requierd")]
  [MinLength(2, ErrorMessage = "Category name must be at least 2 characters long.")]
  [MaxLength(50, ErrorMessage = "Category name must not exceed 50 characters long.")]
  public required string category_name { get; set; }

  public string Slug { get; set; } = string.Empty;

  [MaxLength(300, ErrorMessage = "Description cannot exceed 300 characters long. Please shorten it and try again..")]
  public string description { get; set; } = string.Empty;

  // public ICollection<Product> Products { get; } = new List<Product>();
}