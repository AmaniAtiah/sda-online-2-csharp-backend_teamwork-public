using Microsoft.EntityFrameworkCore;
using Backend.Dtos.Pagination;
using Backend.Dtos;
using Backend.EntityFramework;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using AutoMapper;
using Backend.Helpers;

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
        public async Task<PaginationResult<ProductDtos>> GetAllProductsAsync(int pageNumber, int pageSize, string sortBy, string sortDirection, string searchTerm, List<Guid> selectedCategories, decimal? minPrice, decimal? maxPrice)
        {
            try
            {
                // Validate sortBy and sortDirection parameters
                if (!IsValidSortBy(sortBy) || !IsValidSortDirection(sortDirection))
                {
                    throw new ArgumentException("Invalid sortBy or sortDirection values.");
                }

                var query = _appDbContext.Products.AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(p => p.Name.Contains(searchTerm) ||
                                             p.Description.Contains(searchTerm) ||
                                             p.Brand.Contains(searchTerm));
                }

                if (selectedCategories != null && selectedCategories.Any())
                {
                    query = query.Where(p => selectedCategories.Contains(p.CategoriesId ?? Guid.Empty));
                }

                // Filtering by minPrice and maxPrice
                if (minPrice >= 0 && maxPrice >= minPrice)
                {
                    query = query.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
                }



                // Sorting
                switch (sortBy.ToLower())
                {
                    case "price":
                        query = sortDirection.ToLower() == "desc" ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
                        break;
                    case "name":
                        query = sortDirection.ToLower() == "desc" ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                        break;
                    // case "name":
                    //     query = sortDirection.ToLower() == "desc" ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                    //     break;



                    default:
                        // Default sorting if sortBy parameter is not recognized
                        query = query.OrderBy(p => p.ProductId);
                        break;
                }
                // Searching






                var totalProductCount = await query.CountAsync();

                // Pagination
                var products = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    // products include
                    // .Include(p => p.Category)
                    // .ThenInclude(c => c.Products)


                    .Select(product => new ProductDtos
                    {
                        ProductId = product.ProductId,

                        Name = product.Name,
                        Slug = product.Slug,
                        Description = product.Description,
                        Price = product.Price,
                        Color = product.Color,
                        Quantity = product.Quantity,
                        Image = product.Image,
                        Size = product.Size,
                        Brand = product.Brand,
                        CategoriesId = product.CategoriesId,
                        Category = product.Category,


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
            return sortBy != null && (sortBy.ToLower() == "price" || sortBy.ToLower() == "name");
        }

        // Helper method to validate sortDirection parameter
        private bool IsValidSortDirection(string sortDirection)
        {
            return sortDirection != null && (sortDirection.ToLower() == "asc" || sortDirection.ToLower() == "desc");
        }

        // public async Task<ProductDtos?> GetProductAsync(Guid productId)
        // {
        //     var product = await _appDbContext.Products
        //     .Where(p => p.ProductId == productId)

        //         .Select(p => new ProductDtos
        //         {
        //             Name = p.Name,
        //             Description = p.Description,
        //             Price = p.Price,
        //             Quantity = p.Quantity,
        //             Color = p.Color,

        //             Size = p.Size,

        //             Brand = p.Brand,
        //              CategoriesId = p.CategoriesId,
        //              Category = p.Category



        //         }).FirstOrDefaultAsync();


        //     return product;
        //     //var productDto = _mapper.Map<ProductDtos>(product);
        //     //return product;
        // }


        public async Task<ProductDtos?> GetProductAsync(Guid productId)
        {
            var product = await _appDbContext.Products
            .Where(p => p.ProductId == productId)
                .Select(p => new ProductDtos
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Slug = p.Slug,
                    Description = p.Description,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    Image = p.Image,
                    Color = p.Color,
                    Size = p.Size,
                    Brand = p.Brand,
                    CategoriesId = p.CategoriesId,
                    Category = p.Category,
                    // categorie
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
                Slug = SlugResponse.GenerateSlug(newProduct.Name),
                Description = newProduct.Description,
                Quantity = newProduct.Quantity,
                Price = newProduct.Price,
                Image = newProduct.Image,
                Color = newProduct.Color,
                Size = newProduct.Size,
                Brand = newProduct.Brand,

                CreateAt = DateTime.UtcNow
            };

            var category = await _appDbContext.Categories.FindAsync(newProduct.CategoriesId);
            if (category != null)
            {
                // Associate the product with the category
                product.Category = category;
            }
            else
            {
                // Handle case where category ID is invalid
                // You can throw an exception, log an error, or handle it based on your application logic
            }


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
                existingProduct.Slug = SlugResponse.GenerateSlug(existingProduct.Name);
                existingProduct.Description = updateProduct.Description.IsNullOrEmpty() ? existingProduct.Description : updateProduct.Description;
                existingProduct.Price = updateProduct.Price ?? existingProduct.Price;
                existingProduct.Image = updateProduct.Image.IsNullOrEmpty() ? existingProduct.Image : updateProduct.Image;
                existingProduct.Color = updateProduct.Color.IsNullOrEmpty() ? existingProduct.Color : updateProduct.Color;
                existingProduct.Size = updateProduct.Size.IsNullOrEmpty() ? existingProduct.Size : updateProduct.Size;
                existingProduct.Brand = updateProduct.Brand.IsNullOrEmpty() ? existingProduct.Brand : updateProduct.Brand;
                existingProduct.Quantity = existingProduct.Quantity;

                // Update category relationship if provided
                if (updateProduct.CategoriesId.HasValue)
                {
                    // Check if the category exists
                    var category = await _appDbContext.Categories.FindAsync(updateProduct.CategoriesId);
                    if (category != null)
                    {
                        // Associate the product with the category
                        existingProduct.Category = category;
                    }
                    else
                    {
                        // Handle case where category ID is invalid
                        // You can throw an exception, log an error, or handle it based on your application logic
                    }
                }

                await _appDbContext.SaveChangesAsync();
                return existingProduct;
            }
            throw new Exception("Product not found");
        }


        //          public async Task<Product?> UpdateProductAsync(Guid productId, ProductDtos updateProduct)
        // {
        //     var existingProduct = await _appDbContext.Products.FindAsync(productId);
        //     if (existingProduct != null)
        //     {
        //         existingProduct.Name = updateProduct.Name ?? existingProduct.Name;
        //         existingProduct.Description = updateProduct.Description ?? existingProduct.Description ;
        //         existingProduct.Price = updateProduct.Price ?? existingProduct.Price;
        //         existingProduct.Color = updateProduct.Color ?? existingProduct.Color;
        //         existingProduct.Size = updateProduct.Size ?? existingProduct.Size;
        //         existingProduct.Brand = updateProduct.Brand ?? existingProduct.Brand;
        //         existingProduct.Quantity = existingProduct.Quantity;
        //         existingProduct.CategoriesId = existingProduct.CategoriesId;

        //         await _appDbContext.SaveChangesAsync();
        //     }
        //     return existingProduct;
        // }

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