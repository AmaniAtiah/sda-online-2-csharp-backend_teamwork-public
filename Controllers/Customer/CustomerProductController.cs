using Backend.Dtos;
using Backend.EntityFramework;
using Backend.Models;
using Backend.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("/api/customers/products")]
    // customer can show all products, show product by id and search for product
    //customer can not add or delet and update for product
    public class CustomerProductController : ControllerBase
    {
        private readonly ProductService _productServices;
        public CustomerProductController(ProductService productService)
        {
            _productServices = productService;
        }
        [Authorize(Roles = "User")]
        [HttpGet] //Works 
        public async Task<IActionResult> GetAllProduct([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 3)
        {
            try
            {
                var IsUser = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "User");
                if (!IsUser)
                {
                    return ApiResponse.Forbidden("Only User can visit this route");
                }
                var products = await _productServices.GetAllProductsAsync(pageNumber, pageSize);
                return ApiResponse.Success(products, "All Product are returned successfully");

            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

        //[Authorize]
        [HttpGet("{productId:guid}")] //Works 
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            try
            {
                var IsUser = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "User");
                if (!IsUser)
                {
                    return ApiResponse.Forbidden("Only User can visit this route");
                }
                var product = await _productServices.GetProductAsync(productId);
                if (product != null)
                {
                    return ApiResponse.Success(product, "Product is return successfully");
                }
                return ApiResponse.NotFound("Product was not found");
                //return ApiResponse.Success(product, "All Product are returned successfully");

            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }
        // [HttpPost]
        // public async Task<IActionResult> AddProduct(Product newProduct)
        // {
        //     try
        //     {
        //         var createdProduct = await _productServices.AddProductAsync(newProduct);
        //         return ApiResponse.Created(createdProduct, "Product is added successfully");
        //     }
        //     catch (Exception ex)
        //     {
        //         return ApiResponse.ServerError(ex.Message);
        //     }
        // }

        // [HttpPut("{productId:guid}")]
        // public async Task<IActionResult> UpdateProduct(Guid proudectId, ProductDtos updateProudect)
        // {
        //     try
        //     {
        //         var productToUpdate = await _productServices.UpdateProductAsync(proudectId, updateProudect);
        //         if (productToUpdate == null)
        //         {
        //             return ApiResponse.NotFound("Product was not found");
        //         }
        //         return ApiResponse.Success(productToUpdate, "Product updated successfully");
        //     }
        //     catch (Exception ex)
        //     {
        //         return ApiResponse.ServerError(ex.Message);
        //     }
        // }

        // [HttpDelete("{productId:guid}")]
        // public async Task<IActionResult> DeleteProduct(Guid productId)
        // {
        //     try
        //     {
        //         var result = await _productServices.DeleteUserAsync(productId);
        //         if (!result)
        //         {
        //             return ApiResponse.NotFound("Product was not found");
        //         }
        //         return NoContent();

        //     }
        //     catch (Exception ex)
        //     {
        //         return ApiResponse.ServerError(ex.Message);
        //     }
        // }
        [HttpGet("search")] //Works 
        public async Task<IActionResult> SearchProducts(string keyword)
        {
            try
            {
                var productsFounded = await _productServices.SearchProductByNameAsync(keyword);
                return ApiResponse.Success(productsFounded, "Products are returne successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }

        }
    }
}