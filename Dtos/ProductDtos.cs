namespace Backend.Dtos
{
    public class ProductDtos
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public int? Quantity { get; set; } 
        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
    }
}