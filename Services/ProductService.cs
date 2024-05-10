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
        private readonly AppDbContext _appDbContext;
        public ProductService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _appDbContext.Products.Include(o => o.Orders).ToListAsync();
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
                return await _appDbContext.Products.FindAsync(ProductId);
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
                    CreateAt = DateTime.UtcNow
                };
                await _appDbContext.Products.AddAsync(product);
                await _appDbContext.SaveChangesAsync();
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
                var existingProduct = await _appDbContext.Products.FindAsync(productId);
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
                    await _appDbContext.SaveChangesAsync();
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
                var productToRemove = await _appDbContext.Products.FindAsync(productId);
                if (productToRemove != null)
                {
                    _appDbContext.Products.Remove(productToRemove);
                    await _appDbContext.SaveChangesAsync();
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