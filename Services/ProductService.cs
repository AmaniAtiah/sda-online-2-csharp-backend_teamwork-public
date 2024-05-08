using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Helpers;
using Backend.Models;
using Backend.EntityFramework;

namespace Backend.Services
{
    public class ProductService
    {
        List<Product> products = new List<Product>();
        private readonly AppDbContext _dbContext;
        public ProductService(AppDbContext appcontext)
        {
            _dbContext = appcontext;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _dbContext.Products.Include(o => o.Orders).ToListAsync();
            }

            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while retrieving product.", e);
            }
        }
        public async Task<Product?> GetProductAsync(Guid ProductId)
        {
            try
            {
                return await _dbContext.Products.FindAsync(ProductId);
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while retrieving product.", e);
            }

        }
        public async Task<Product> AddProductAsync(Product newProduct)
        {
            try
            {
                Product product = new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = newProduct.Name,
                    Description = newProduct.Description,
                    Quantity = newProduct.Quantity,
                    Price = newProduct.Price,
                    CategoriesId = newProduct.CategoriesId,
                    OrderId = newProduct.OrderId,
                    CreateAt = DateTime.UtcNow
                };
                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                return product;
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while adding product.", e);
            }

        }
        public async Task<Product?> UpdateProductAsync(Guid productId, Product updateProduct)
        {
            try
            {
                var existingProduct = await _dbContext.Products.FindAsync(productId);
                if (existingProduct != null)
                {
                    existingProduct.Name = updateProduct.Name ?? existingProduct.Name;
                    existingProduct.Description = updateProduct.Description ?? existingProduct.Description;
                    existingProduct.Price = updateProduct.Price ?? existingProduct.Price;
                    existingProduct.Color = updateProduct.Color ?? existingProduct.Color;
                    existingProduct.Size = updateProduct.Size ?? existingProduct.Size;
                    existingProduct.Brand = updateProduct.Brand ?? existingProduct.Brand;
                    existingProduct.Quantity = updateProduct.Quantity ?? existingProduct.Quantity;
                    existingProduct.CategoriesId = updateProduct.CategoriesId;
                    existingProduct.OrderId = updateProduct.OrderId;
                    await _dbContext.SaveChangesAsync();
                    return existingProduct;
                }
                throw new Exception("Product not found");
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while updating user.", e);

            }
        }

        public async Task<bool> DeleteUserAsync(Guid productId)
        {
            try
            {
                var productToRemove = await _dbContext.Products.FindAsync(productId);
                if (productToRemove != null)
                {
                    _dbContext.Products.Remove(productToRemove);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                throw new Exception("Product not found");
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while deleting product.", e);

            }
        }
    }
}
// using Microsoft.EntityFrameworkCore;
// using Backend.EntityFramework;
// using Backend.Helpers;
// public class ProductService
// {
//     List<Product> products = new List<Product>();
//     private readonly AppDbContext _dbContext;
//     public ProductService(AppDbContext appcontext)
//     {
//         _dbContext = appcontext;
//     }
//     public async Task<IEnumerable<Product>> GetAllProductsAsync()
//     {
//         try
//         {
//             return await _dbContext.Products.ToListAsync();
//         }
//         catch (Exception e)
//         {
//             throw new ApplicationException("An error occurred while retrieving users.", e);
//         }
//     }
//     public Product? FindProductById(Guid id)
//     {
//         return products.Find(product => product.ProductId == id);
//     }
//     public Product CreateProductService(Product newProduct)
//     {
//         //Step1: create record:
//         Product product = new Product
//         {
//             Name = newProduct.Name,
//             Description = newProduct.Description,
//             Price = newProduct.Price,
//             Color = newProduct.Color,
//             Size = newProduct.Size,
//             Brand = newProduct.Brand,
//             Quantity = newProduct.Quantity
//             //CategoriesId = newProduct.CategoriesId
//             // CategoriesId = newProduct.CategoriesId,
//         };
//         //Step2: Add the record to the context:
//         _dbContext.Products.Add(newProduct);
//         _dbContext.SaveChanges();
//         return newProduct;
//     }
//     public Product? UpdateProductService(Guid productId, Product updateProduct)
//     {
//         var existingProduct = _dbContext.Products.FirstOrDefault(p => p.ProductId == productId);
//         if (existingProduct != null)
//         {
//             existingProduct.Name = updateProduct.Name ?? existingProduct.Name;
//             existingProduct.Description = updateProduct.Description ?? existingProduct.Description;
//             existingProduct.Price = updateProduct.Price ?? existingProduct.Price;
//             existingProduct.Color = updateProduct.Color ?? existingProduct.Color;
//             existingProduct.Size = updateProduct.Size ?? existingProduct.Size;
//             existingProduct.Brand = updateProduct.Brand ?? existingProduct.Brand;
//             existingProduct.Quantity = updateProduct.Quantity ?? existingProduct.Quantity;
//             _dbContext.Products.Add(updateProduct);
//             _dbContext.SaveChanges();

//         }
//         return existingProduct;
//     }

//     public bool DeleteProductService(Guid productId)
//     {
//         var productToRemove = _dbContext.Products.FirstOrDefault(p => p.ProductId == productId);
//         if (productToRemove != null)
//         {
//             _dbContext.Products.Remove(productToRemove);
//             _dbContext.SaveChanges();
//             return true;
//         }
//         return false;
//     }
// }