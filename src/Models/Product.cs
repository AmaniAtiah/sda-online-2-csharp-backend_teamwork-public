public class Product
{
    public Guid ProductsId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public string Color { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public required int Quantity { get; set; }
    public DateTime CreateAt { get; set; }
    //Adding categories Id
}
