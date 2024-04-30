public class ProductService
{
    //user api
    public static List<Product> products = new List<Product>(){
    new Product {
    ProductsId = Guid.NewGuid(),
    Name = "KAREN MILLEN DRESS",
    Description = "Main: 62% Polyester, 34% Viscose/Rayon, 4% Elastane/Spandex. Lining: 100% Polyester. Dry Clean Only. Model wears UK 8/US 4. Model height: 59. Length measurement: 113cm.",
    Price = 1303,
    Color = "Black",
    Size = "S",
    Brand = "KAREN MILLEN",
    Quantity = 100
    //Categorie id

    },
    new Product {
    ProductsId = Guid.NewGuid(),
    Name = "BOOHOO NECKLACE AND EARRING SET",
    Description = "Material: 40% ZINC, 30% IRON, 20% GLASS + 10% CCB",
    Price = 53,
    Color = "SILVER",
    Size = "l",
    Brand = "BOOHOO",
    Quantity = 5
    //Categorie id
    },
};

    public IEnumerable<Product> GetAllProducts()
    {
        return products;
    }
    public Product? FindProductById(Guid id)
    {
        return products.Find(product => product.ProductsId == id);
    }
}