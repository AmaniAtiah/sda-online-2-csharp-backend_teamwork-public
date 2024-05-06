using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



using api.Data;

using api.Helpers;

using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
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
        public IActionResult GetAllProductControllers()
        {
            try
            {
                var products = _productServices.GetAllProducts();
                if (products.ToList().Count < 1)
                {
                    return NotFound(new ErrorResponse { Message = "There Is No Product Found ..." });
                }
                return Ok(new SuccessResponse<IEnumerable<Product>>
                {
                    Message = "Return All Product Successfully.",
                    Data = products
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Message = ex.Message });
            }
        }
        [HttpGet("{productId:guid}")]
        public IActionResult GetAllProductByIdControllers(string proudectId)
        {
            try
            {
                if (!Guid.TryParse(proudectId, out Guid productIdGuid))
                {
                    return BadRequest("Invalid Product id try again ...");
                }
                var product = _productServices.FindProductById(productIdGuid);
                return Ok(new SuccessResponse<Product>
                {
                    Message = "Return Single Product Successfully.",
                    Data = product
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Message = ex.Message });
            }
        }
        [HttpPost]
        public IActionResult CreateCategory(Product newProduct)
        {
            try
            {
                var createdProduct = _productServices.CreateProductService(newProduct);
                return CreatedAtAction(nameof(GetAllProductControllers), new { proudctId = createdProduct.ProductId }, createdProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Message = ex.Message });
            }
        }

        [HttpPut("{productId:guid}")]
        public IActionResult UpdateProduct(string proudectId, Product updateProudect)
        {
            try
            {
                if (!Guid.TryParse(proudectId, out Guid proudectIdGuid))
                {
                    return BadRequest("Invalid Product ID Format");
                }
                var productToUpdate = _productServices.UpdateProductService(proudectIdGuid, updateProudect);
                if (productToUpdate == null)
                {
                    return NotFound(new ErrorResponse { Message = "The Product Is Not Found To Update ..." });
                }
                return Ok(new SuccessResponse<Product>
                {
                    Message = "Update User Successfully.",
                    Data = productToUpdate
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Message = ex.Message });
            }
        }

        [HttpDelete("{productId:guid}")]
        public IActionResult DeleteProduct(string productId)
        {
            try
            {
                if (!Guid.TryParse(productId, out Guid productIdGuid))
                {
                    return BadRequest("Invalid Product ID Format");
                }
                var result = _productServices.DeleteProductService(productIdGuid);
                if (!result)
                {
                    return NotFound(new ErrorResponse { Message = "The Product Is Not Found To Delete ..." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Message = ex.Message });
            }
        }
    }
}