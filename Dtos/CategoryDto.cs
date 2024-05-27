
namespace Backend.Dtos
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; } 
        public string? Description { get; set; }

        // list of products


 public  List<ProductDtos> Products { get; set; } = new List<ProductDtos>();
    }
}