using System.Security.Claims;
using Backend.Dtos;
using Backend.EntityFramework;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
   
    [ApiController]
    [Route("/api/products")]
    // admin can show all products and add or delete 

    public class ProductController : ControllerBase
    {
        private readonly ProductService _productServices;
        public ProductController(ProductService productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 3, [FromQuery] string sortBy = "price", [FromQuery] string sortDirection = "asc", [FromQuery] string searchTerm = "", [FromQuery] List<Guid> selectedCategories = null, [FromQuery] decimal? minPrice = null , [FromQuery] decimal? maxPrice = null) 
        {
            try
            {
               
                var products = await _productServices.GetAllProductsAsync(pageNumber, pageSize, sortBy, sortDirection, searchTerm, selectedCategories, minPrice, maxPrice);
                return ApiResponse.Success(products, "All Product are returned successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }
 

        [HttpGet("{productId:guid}")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            try
            {
                
                var product = await _productServices.GetProductAsync(productId);
                if (product != null)
                {
                    return ApiResponse.Success(product, "Product is retrieved successfully");
                }
                else
                {
                    return ApiResponse.NotFound("product was not found");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

         [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product newProduct)
        {
            try
            {
            
                       if (!ModelState.IsValid)
            {
                ApiResponse.BadRequest("invalid product data provided");
            }
                var createdProduct = await _productServices.AddProductAsync(newProduct);
                return ApiResponse.Created(createdProduct, "Product is added successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }


          [Authorize(Roles = "Admin")]
          [HttpPut("{productId:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid productId, ProductDtos updateProudect)
        {
            try
            {
                // var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                // if (!isAdmin)
                // {
                //     return ApiResponse.Forbidden("Only admin can visit this route");
                // }

                       if (!ModelState.IsValid)
            {
                ApiResponse.BadRequest("invalid product data provided");
            }
                var product = await _productServices.UpdateProductAsync(productId, updateProudect);
                if (product == null)
                {
                    return ApiResponse.NotFound("product was not found");
                }
                else
                {
                    return ApiResponse.Success(product, "Update product successfully");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

         [Authorize(Roles = "Admin")]
        [HttpDelete("{productId:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            try
            {
                // var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                // if (!isAdmin)
                // {
                //     return ApiResponse.Forbidden("Only admin can visit this route");
                // }
                       if (!ModelState.IsValid)
            {
                ApiResponse.BadRequest("invalid product data provided");
            }
                var result = await _productServices.DeleteUserAsync(productId);
                if (!result)
                {
                    return ApiResponse.NotFound("Product was not found");
                }
                return NoContent();

            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }
        
        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts(string searchkeyword)
        {
            try
            {
                var productsFounded = await _productServices.SearchProductByNameAsync(searchkeyword);
                return ApiResponse.Success(productsFounded, "Products are returne successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }

        }
    }
}