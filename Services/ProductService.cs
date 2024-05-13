using Microsoft.EntityFrameworkCore;
using Backend.Dtos.Pagination;
using Backend.Models;
using Backend.Dtos;
using Backend.EntityFramework;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using AutoMapper;

namespace Backend.Services
{
    public class ProductService
    {
        List<Product> products = new List<Product>();
        private readonly AppDbContext _appDbContext;

        private readonly IMapper _mapper;
        public ProductService(AppDbContext appcontext, IMapper mapper)
        {
            _appDbContext = appcontext;
            _mapper = mapper;
        }
        public async Task<PaginationResult<ProductDtos>> GetAllProductsAsync(int pageNumber, int pageSize, string sortBy, string sortDirection)
        {
            try
            {
                // Validate sortBy and sortDirection parameters
                if (!IsValidSortBy(sortBy) || !IsValidSortDirection(sortDirection))
                {
                    throw new ArgumentException("Invalid sortBy or sortDirection values.");
                }

                var query = _appDbContext.Products.AsQueryable();

                // Sorting
                switch (sortBy.ToLower())
                {
                    case "price":
                        query = sortDirection.ToLower() == "desc" ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
                        break;
                    case "date":
                        query = sortDirection.ToLower() == "desc" ? query.OrderBy(p => p.CreateAt) : query.OrderByDescending(p => p.CreateAt);
                        break;
                    default:
                        // Default sorting if sortBy parameter is not recognized
                        query = query.OrderBy(p => p.ProductId);
                        break;
                }

                var totalProductCount = await query.CountAsync();

                // Pagination
                var products = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(product => new ProductDtos
                    {
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Color = product.Color,
                        Size = product.Size,
                        Brand = product.Brand
                    })
                    .ToListAsync();

                return new PaginationResult<ProductDtos>
                {
                    Items = products,
                    TotalCount = totalProductCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching products. Please try again later.", ex);
            }
        }

        // Helper method to validate sortBy parameter
        private bool IsValidSortBy(string sortBy)
        {
            return sortBy != null && (sortBy.ToLower() == "price" || sortBy.ToLower() == "date");
        }

        // Helper method to validate sortDirection parameter
        private bool IsValidSortDirection(string sortDirection)
        {
            return sortDirection != null && (sortDirection.ToLower() == "asc" || sortDirection.ToLower() == "desc");
        }

        public async Task<ProductDtos?> GetProductAsync(Guid productId)
        {
            var product = await _appDbContext.Products
            .Where(p => p.ProductId == productId)
                .Select(p => new ProductDtos
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Color = p.Color,
                    Size = p.Size,
                    Brand = p.Brand,
                }).FirstOrDefaultAsync();
            return product;
            //var productDto = _mapper.Map<ProductDtos>(product);
            //return product;
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