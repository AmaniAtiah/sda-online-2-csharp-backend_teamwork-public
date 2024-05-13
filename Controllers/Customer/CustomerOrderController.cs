using System.Security.Claims;
using Backend.EntityFramework;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]//api controllers
    [Route("/api/customers/orders")] // for httpget
    // customer can show all Orders, show Orders by id and create orders and add product to order
    //customer can not update or delete but can cancel
    public class CustomerOrderController : ControllerBase
    {
        private readonly OrderService _orderServices;
        public CustomerOrderController(AppDbContext appDbContext)
        {
            _orderServices = new OrderService(appDbContext);
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()//how can show for user only his order
        {
            try
            {
                var isUser = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "User");
                if (!isUser)
                {
                    return ApiResponse.Forbidden("Only User can visit this route");
                }
                var orders = await _orderServices.GetAllOrdersAsync();
                if (orders.ToList().Count < 1)
                {
                    return ApiResponse.NotFound("No order found");
                }
                return ApiResponse.Success(orders, "all orders retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{orderId:guid}")]
        public async Task<IActionResult> GetOrderById(Guid orderId, Guid userId)//
        {
            try
            {
                var isUser = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "User");
                if (!isUser)
                {
                    return ApiResponse.Forbidden("Only User can visit this route");
                }
                var order = await _orderServices.GetOrderAsync(orderId, userId);//
                if (order == null)
                {
                    return ApiResponse.NotFound("Order was not found");
                }
                return ApiResponse.Created(order);
            }

            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrder(Order newOrder, Guid userId)//
        {
            try
            {
                var isUser = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "User");
                if (!isUser)
                {
                    return ApiResponse.Forbidden("Only User can visit this route");
                }
                var createdOrder = await _orderServices.AddOrderAsync(newOrder, userId);//
                return ApiResponse.Created(createdOrder);
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("{orderId:guid}")]
        public async Task<IActionResult> AddProductToOrder(Guid orderId, Guid productId, Guid userId)//
        {
            try
            {
                var isUser = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "User");
                if (!isUser)
                {
                    return ApiResponse.Forbidden("Only User can visit this route");
                }
                await _orderServices.AddProductToOrder(orderId, productId, userId);//
                return ApiResponse.Created("Products Added to the order successfully");
            }
            catch (Exception e)
            {
                return ApiResponse.ServerError(e.Message);
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