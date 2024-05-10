using Backend.EntityFramework;
using Backend.Helpers;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]//api controllers
    [Route("/api/products")] // for httpget
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productServices;
        public ProductController(AppDbContext appDbContext)
        {
            _productServices = new ProductService(appDbContext);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 3)
        {
            try
            {
                var products = await _productServices.GetAllProductsAsync(pageNumber, pageSize);
                return ApiResponse.Success(products, "All Product are returned successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }
        [HttpGet("{productId:guid}")]
        public async Task<IActionResult> GetProductById(Guid proudectId)
        {
            try
            {
                var product = await _productServices.GetProductAsync(proudectId);
                if (product == null)
                {
                    return ApiResponse.NotFound("Product was not found");
                }
                return ApiResponse.Created(product);
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product newProduct)
        {
            try
            {
                var createdProduct = await _productServices.AddProductAsync(newProduct);
                return ApiResponse.Created(createdProduct);
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

        [HttpPut("{productId:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid proudectId, Product updateProudect)
        {
            try
            {
                var productToUpdate = await _productServices.UpdateProductAsync(proudectId, updateProudect);
                if (productToUpdate == null)
                {
                    return ApiResponse.NotFound("Product was not found");
                }
                return ApiResponse.Success(productToUpdate, "Product updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

        [HttpDelete("{productId:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            try
            {
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
        [HttpGet("searchkeyword")]
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
