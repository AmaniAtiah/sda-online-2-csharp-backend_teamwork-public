using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Helpers;
public class ProductService
{
    //     //user api
    //     public static List<Product> products = new List<Product>(){
    //     new Product {
    //     ProductsId = Guid.NewGuid(),
    //     Name = "KAREN MILLEN DRESS",
    //     Description = "Main: 62% Polyester, 34% Viscose/Rayon, 4% Elastane/Spandex. Lining: 100% Polyester. Dry Clean Only. Model wears UK 8/US 4. Model height: 59. Length measurement: 113cm.",
    //     Price = 1303,
    //     Color = "Black",
    //     Size = "S",
    //     Brand = "KAREN MILLEN",
    //     Quantity = 100
    //     //Categorie id

    //     },
    //     new Product {
    //     ProductsId = Guid.NewGuid(),
    //     Name = "BOOHOO NECKLACE AND EARRING SET",
    //     Description = "Material: 40% ZINC, 30% IRON, 20% GLASS + 10% CCB",
    //     Price = 53,
    //     Color = "SILVER",
    //     Size = "l",
    //     Brand = "BOOHOO",
    //     Quantity = 5
    //     //Categorie id
    //     },
    // };
    List<Product> products = new List<Product>();
    private readonly AppDbContext _dbContext;
    public ProductService(AppDbContext appcontext)
    {
        _dbContext = appcontext;
    }
    public IEnumerable<Product> GetAllProducts()
    {
        var dataList = _dbContext.Products.ToList();
        dataList.ForEach(row => products.Add(new Product
        {
            ProductId = row.ProductId,
            Name = row.Name,
            Description = row.Description,
            Price = row.Price,
            Color = row.Color,
            Size = row.Size,
            Brand = row.Brand,
            Quantity = row.Quantity,
            CategoriesId = row.CategoriesId,
            Categories = new Categories
            {
                category_id = row.Categories.category_id,
                category_name = row.Categories.category_name,
                description = row.Categories.description
            }

        }));
        return products;
    }
    public Product? FindProductById(Guid id)
    {
        return products.Find(product => product.ProductId == id);
    }
    public Product CreateProductService(Product newProduct)
    {
        //Step1: create record:
        Product product = new Product
        {
            ProductId = Guid.NewGuid(),
            Name = newProduct.Name,
            Description = newProduct.Description,
            Price = newProduct.Price,
            Color = newProduct.Color,
            Size = newProduct.Size,
            Brand = newProduct.Brand,
            Quantity = newProduct.Quantity,
            CategoriesId = newProduct.CategoriesId,
        };
        //Step2: Add the record to the context:
        _dbContext.Products.Add(newProduct);
        _dbContext.SaveChanges();
        return newProduct;
    }
    public Product? UpdateProductService(Guid productId, Product updateProduct)
    {
        var existingProduct = _dbContext.Products.FirstOrDefault(p => p.ProductId == productId);
        if (existingProduct != null)
        {
            existingProduct.Name = updateProduct.Name ?? existingProduct.Name;
            existingProduct.Description = updateProduct.Description ?? existingProduct.Description;
            existingProduct.Price = updateProduct.Price ?? existingProduct.Price;
            existingProduct.Color = updateProduct.Color ?? existingProduct.Color;
            existingProduct.Size = updateProduct.Size ?? existingProduct.Size;
            existingProduct.Brand = updateProduct.Brand ?? existingProduct.Brand;
            existingProduct.Quantity = updateProduct.Quantity ?? existingProduct.Quantity;
            _dbContext.Products.Add(updateProduct);
            _dbContext.SaveChanges();

        }
        return existingProduct;
    }

    public bool DeleteProductService(Guid productId)
    {
        var productToRemove = _dbContext.Products.FirstOrDefault(p => p.ProductId == productId);
        if (productToRemove != null)
        {
            _dbContext.Products.Remove(productToRemove);
            _dbContext.SaveChanges();
            return true;
        }
        return false;
    }
}