using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]//api controllers
    [Route("/api/products")] // for httpget
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productServices;
        public ProductController()
        {
            _productServices = new ProductService();
        }
        [HttpGet]
        public IActionResult GetAllProductControllers()
        {
            var products = _productServices.GetAllProducts();
            return Ok(products);
        }
        [HttpGet("{productId}")]
        public IActionResult GetAllProductControllers(string proudectId)
        {
            if (!Guid.TryParse(proudectId, out Guid productIdGuid))
            {
                return BadRequest("Invalid product id try again ...");
            }
            var product = _productServices.FindProductById(productIdGuid);
            return Ok(product);
        }
        [HttpPost]
        public IActionResult CreateCategory(Product newProduct)
        {
            var createdProduct = _productServices.CreateProductService(newProduct);
            return CreatedAtAction(nameof(GetAllProductControllers), new { proudctId = createdProduct.ProductsId }, createdProduct);
        }

        [HttpPut("{productId}")]
        public IActionResult UpdateProduct(string proudectId, Product updateProudect)
        {
            if (!Guid.TryParse(proudectId, out Guid proudectIdGuid))
            {
                return BadRequest("Invalid Product ID Format");
            }
            var productToUpdate = _productServices.UpdateProductService(proudectIdGuid, updateProudect);
            if (productToUpdate == null)
            {
                return NotFound();
            }
            return Ok(productToUpdate);
        }

        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(string productId)
        {
            if (!Guid.TryParse(productId, out Guid productIdGuid))
            {
                return BadRequest("Invalid Product ID Format");
            }
            var result = _productServices.DeleteProductService(productIdGuid);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}