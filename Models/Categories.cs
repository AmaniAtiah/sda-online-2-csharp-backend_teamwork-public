using System.ComponentModel.DataAnnotations;

public class Categories
{
  public Guid category_id { get; set; }

  [Required(ErrorMessage = "Category name is requierd")]
  [MaxLength(100), MinLength(2)]
  public required string category_name { get; set; }

  [MaxLength(300)]
  public string description { get; set; } = string.Empty;

}