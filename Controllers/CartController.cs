using System.Security.Claims;
using Backend.Dtos;
using Backend.EntityFramework;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]//api controllers
    [Route("/api/carts")] // for httpget
    // customer can show all Orders, show Orders by id and create orders and add product to order
    //customer can not update or delete but can cancel
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;
        
        public CartController(CartService cartService)
        {
             _cartService = cartService;

        }

        // [Authorize]
        // [HttpPost]
        // public async Task<IActionResult> AddCart(Guid userId)//
        // {
        //     try
        //     {
        //         var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //     if (string.IsNullOrEmpty(userIdString))
        //     {
        //         return ApiResponse.UnAuthorized("User Id is misisng from token");
        //     }

        //     if (!Guid.TryParse(userIdString, out userId))
        //     {
        //         return ApiResponse.BadRequest("Invalid User Id");
        //     }

                
        //         var createdCart = await _cartService.AddCartAsync(userId);//
        //         return ApiResponse.Created(createdCart);
        //     }
        //     catch (Exception ex)
        //     {
        //         return ApiResponse.ServerError(ex.Message);
        //     }
        // }

        // add products
      
    
        
        // [HttpPost("add-product")]
        // public async Task<IActionResult> AddProductToCart(Guid cartId, Guid productId, int quantity)
        // {
        //     try
        //     {
        //         // var isUser = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "User");
        //         // if (!isUser)
        //         // {
        //         //     return ApiResponse.UnAuthorized("You are not authorized");
        //         // }
        //         // var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //         // if (string.IsNullOrEmpty(userIdString))
        //         // {
        //         //     return ApiResponse.UnAuthorized("User Id is misisng from token");
        //         // }
        //         // if (!Guid.TryParse(userIdString, out Guid userId))
        //         // {
        //         //     return ApiResponse.BadRequest("Invalid User Id");
        //         // }
                
        //         await _cartService.AddProductToCartAsync(cartId, productId, quantity);
        //         return ApiResponse.Success("Product added to cart successfully");
                
        //     }
        //     catch (Exception ex)
        //     {
        //         return ApiResponse.ServerError(ex.Message);
        //     }
        // }


        // display cart own user 
        // [Authorize]
         [HttpGet("{userId:guid}/{cartId:guid}")]
        public async Task<IActionResult> GetCart(Guid cartId, Guid userId)
        {
               try
            {
                

                var cart = await _cartService.GetCartAsync(cartId, userId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            } 
        }

        // delete product from cart 
        // [Authorize]
        [HttpDelete("{userId:guid}/{cartId:guid}/{productId:guid}")]
        public async Task<IActionResult> DeleteProductFromCart(Guid cartId, Guid productId, Guid userId)
        {
            try
            {
                // user login 
                // var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                // if (string.IsNullOrEmpty(userIdString))
                // {
                //     return ApiResponse.UnAuthorized("User Id is misisng from token");
                // }
                // if (!Guid.TryParse(userIdString, out Guid userId))
                // {
                //     return ApiResponse.BadRequest("Invalid User Id");
                // }

                       if (!ModelState.IsValid)
            {
                ApiResponse.BadRequest("invalid cart data provided");
            }

                await _cartService.DeleteProductFromCartAsync(cartId, productId, userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


//    [Authorize]

         [HttpPost("{userId:guid}")]
        public async Task<IActionResult> CreateAndAddProductToCart([FromBody]CreateCartProductDto cartProductDTO, Guid userId)
        {
            try
            {
                // user login 
                // var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                // if (string.IsNullOrEmpty(userIdString))
                // {
                //     return ApiResponse.UnAuthorized("User Id is misisng from token");
                // }
                // if (!Guid.TryParse(userIdString, out Guid userId))
                // {
                //     return ApiResponse.BadRequest("Invalid User Id");
                // }
        
               if (!ModelState.IsValid)
            {
                ApiResponse.BadRequest("invalid cart data provided");
            }
                
                if (cartProductDTO == null)
                    return BadRequest("Invalid request");

                var cart = await _cartService.CreateCartAndAddProductAsync(cartProductDTO, userId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}


//         [Authorize]
//         [HttpPut("{orderId:guid}")]
//         public async Task<IActionResult> UpdateOrder(Guid orderId, Order updateOrder)
//         {
//             try
//             {
//                 var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//                 if (string.IsNullOrEmpty(userIdString))
//                 {
//                     return ApiResponse.UnAuthorized("User Id is misisng from token");
//                 }
//                 if (!Guid.TryParse(userIdString, out Guid userId))
//                 {
//                     return ApiResponse.BadRequest("Invalid User Id");
//                 }
//                 var updateToOrder = await _orderServices.UpdateOrdertAsync(orderId, updateOrder, userId);
//                 if (updateToOrder == null)
//                 {
//                     return ApiResponse.NotFound("Order was not found");
//                 }
//                 return ApiResponse.Success(updateToOrder, "Order updated successfully");
//             }
//             catch (Exception ex)
//             {
//                 return ApiResponse.ServerError(ex.Message);
//             }
//         }

//         [Authorize]
//         [HttpDelete("{orderId:guid}")]
//         public async Task<IActionResult> DeleteOrder(Guid orderId)
//         {
//             try
//             {
//                 var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//                 if (string.IsNullOrEmpty(userIdString))
//                 {
//                     return ApiResponse.UnAuthorized("User Id is misisng from token");
//                 }
//                 if (!Guid.TryParse(userIdString, out Guid userId))
//                 {
//                     return ApiResponse.BadRequest("Invalid User Id");
//                 }
//                 var result = await _orderServices.DeleteOrderAsync(orderId, userId);
//                 if (!result)
//                 {
//                     return ApiResponse.NotFound("Order was not found");
//                 }
//                 return NoContent();

//             }
//             catch (Exception ex)
//             {
//                 return ApiResponse.ServerError(ex.Message);
//             }
//         }
//     }
// }