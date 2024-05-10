
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
        public async Task<IActionResult> GetAllProductControllers()
        {
            try
            {
                var products = await _productServices.GetAllProductsAsync();
                if (products.ToList().Count < 1)
                {
                    return ApiResponse.NotFound("No product found");
                }
                return ApiResponse.Success(products, "all products retrieved successfully");
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

// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Backend.EntityFramework;
// using Backend.Helpers;
// using Microsoft.AspNetCore.Mvc;

// namespace api.Controllers
// {
//     [ApiController]//api controllers
//     [Route("/api/products")] // for httpget
//     public class ProductController : ControllerBase
//     {
//         private readonly ProductService _productServices;
//         public ProductController(AppDbContext appDbContext)
//         {
//             _productServices = new ProductService(appDbContext);
//         }
//         [HttpGet]
//         public async Task<IActionResult> GetAllProductControllers()
//         {
//             try
//             {
//                 var products = await _productServices.GetAllProductsAsync();
//                 if (products.ToList().Count < 1)
//                 {
//                     return ApiResponse.NotFound("No Product found");
//                 }
//                 return ApiResponse.Success(products, "All products are returned");
//             }
//             catch (Exception ex)
//             {
//                 return ApiResponse.ServerError(ex.Message);
//             }

//         }
//         [HttpGet("{productId:guid}")]
//         public IActionResult GetAllProductByIdControllers(string proudectId)
//         {
//             try
//             {
//                 if (!Guid.TryParse(proudectId, out Guid productIdGuid))
//                 {
//                     return BadRequest("Invalid Product id try again ...");
//                 }
//                 var product = _productServices.FindProductById(productIdGuid);
//                 return Ok(new SuccessResponse<Product>
//                 {
//                     Message = "Return Single Product Successfully.",
//                     Data = product
//                 });
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, new ErrorResponse { Message = ex.Message });
//             }
//         }
//         [HttpPost]
//         public IActionResult CreateProduct(Product newProduct)
//         {
//             try
//             {
//                 var createdProduct = _productServices.CreateProductService(newProduct);
//                 return CreatedAtAction(nameof(GetAllProductControllers), new { proudctId = createdProduct.ProductId }, createdProduct);
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, new ErrorResponse { Message = ex.Message });
//             }
//         }

//         [HttpPut("{productId:guid}")]
//         public IActionResult UpdateProduct(string proudectId, Product updateProudect)
//         {
//             try
//             {
//                 if (!Guid.TryParse(proudectId, out Guid proudectIdGuid))
//                 {
//                     return BadRequest("Invalid Product ID Format");
//                 }
//                 var productToUpdate = _productServices.UpdateProductService(proudectIdGuid, updateProudect);
//                 if (productToUpdate == null)
//                 {
//                     return NotFound(new ErrorResponse { Message = "The Product Is Not Found To Update ..." });
//                 }
//                 return Ok(new SuccessResponse<Product>
//                 {
//                     Message = "Update User Successfully.",
//                     Data = productToUpdate
//                 });
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, new ErrorResponse { Message = ex.Message });
//             }
//         }

//         [HttpDelete("{productId:guid}")]
//         public IActionResult DeleteProduct(string productId)
//         {
//             try
//             {
//                 if (!Guid.TryParse(productId, out Guid productIdGuid))
//                 {
//                     return BadRequest("Invalid Product ID Format");
//                 }
//                 var result = _productServices.DeleteProductService(productIdGuid);
//                 if (!result)
//                 {
//                     return NotFound(new ErrorResponse { Message = "The Product Is Not Found To Delete ..." });
//                 }
//                 return NoContent();
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, new ErrorResponse { Message = ex.Message });
//             }
//         }
//     }
// }