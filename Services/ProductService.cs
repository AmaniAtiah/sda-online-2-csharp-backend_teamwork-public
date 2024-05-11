using Microsoft.EntityFrameworkCore;
using Backend.Dtos.Pagination;
using Backend.Models;
using Backend.Dtos;
using Backend.EntityFramework;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services
{
    public class ProductService
    {
        List<Product> products = new List<Product>();
        private readonly AppDbContext _appDbContext;
        public ProductService(AppDbContext appcontext)
        {
            _appDbContext = appcontext;
        }
        public async Task<PaginationResult<ProductDtos>> GetAllProductsAsync(int pageNumber, int pageSize)
        {
            var totalProductAccount = await _appDbContext.Products.CountAsync();
            var products = await _appDbContext.Products
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(product => new ProductDtos
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Color = product.Color,
                Size = product.Size,
                Brand = product.Brand,
            })
            .ToListAsync();
            return new PaginationResult<ProductDtos>
            {
                Items = products,
                TotalCount = totalProductAccount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<Product?> GetProductAsync(Guid ProductId)
        {
            return await _appDbContext.Products.FindAsync(ProductId);
        }

        public async Task<Product> AddProductAsync(Product newProduct)
        {
            Product product = new Product
            {
                ProductId = Guid.NewGuid(),
                Name = newProduct.Name,
                Description = newProduct.Description,
                Quantity = newProduct.Quantity,
                Price = newProduct.Price,
                Color = newProduct.Color,
                Size = newProduct.Size,
                Brand = newProduct.Brand,
                CategoriesId = newProduct.CategoriesId,
                CreateAt = DateTime.UtcNow
            };
            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateProductAsync(Guid productId, ProductDtos updateProduct)
        {
            var existingProduct = await _appDbContext.Products.FindAsync(productId);
            if (existingProduct != null)
            {
                existingProduct.Name = updateProduct.Name.IsNullOrEmpty() ? existingProduct.Name : updateProduct.Name;
                existingProduct.Description = updateProduct.Description.IsNullOrEmpty() ? existingProduct.Description : updateProduct.Description;
                existingProduct.Price = updateProduct.Price ?? existingProduct.Price;
                existingProduct.Color = updateProduct.Color.IsNullOrEmpty() ? existingProduct.Color : updateProduct.Color;
                existingProduct.Size = updateProduct.Size.IsNullOrEmpty() ? existingProduct.Size : updateProduct.Size;
                existingProduct.Brand = updateProduct.Brand.IsNullOrEmpty() ? existingProduct.Brand : updateProduct.Brand;
                existingProduct.Quantity = existingProduct.Quantity;
                existingProduct.CategoriesId = existingProduct.CategoriesId;
                await _appDbContext.SaveChangesAsync();
                return existingProduct;
            }
            throw new Exception("Product not found");
        }

        public async Task<bool> DeleteUserAsync(Guid productId)
        {
            var productToRemove = await _appDbContext.Products.FindAsync(productId);
            if (productToRemove != null)
            {
                _appDbContext.Products.Remove(productToRemove);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Product?>> SearchProductByNameAsync(string searchKeyword)
        {
            if (_appDbContext.Products == null)
            {
                throw new InvalidOperationException("Product not found");
            }
            var foundProducts = await _appDbContext.Products
            .Where(product => product.Name.Contains(searchKeyword) || product.Name.Contains(searchKeyword)).ToListAsync();
            return foundProducts;
        }
    }
}