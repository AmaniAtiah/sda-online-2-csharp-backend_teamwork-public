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

    }
}