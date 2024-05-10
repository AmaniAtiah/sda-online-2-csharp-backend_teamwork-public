
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
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var products = await _productServices.GetAllProductsAsync();
                if (products.ToList().Count < 1)
                {
                    return ApiResponse.NotFound("No product found");
                }
                return ApiResponse.Success(products, "All product are returned");
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
    }
}