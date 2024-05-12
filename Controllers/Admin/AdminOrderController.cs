using System.Security.Claims;
using Backend.EntityFramework;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]//api controllers
    [Route("/api/admin/orders")] // for httpget
    // admin can show all orders and update status of order
    public class AdminOrderController : ControllerBase
    {
        private readonly OrderService _orderServices;
        public AdminOrderController(AppDbContext appDbContext)
        {
            _orderServices = new OrderService(appDbContext);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                if (!isAdmin)
                {
                    return ApiResponse.Forbidden("Only admin can visit this route");
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
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            try
            {
                  var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                if (!isAdmin)
                {
                    return ApiResponse.Forbidden("Only admin can visit this route");
                }

                var order = await _orderServices.ShowAddressByAdmin(orderId);
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
        
  
////////////////////////////////
        [HttpPut("{orderId:guid}")]
        public async Task<IActionResult> UpdateOrder(Guid orderId, Order updateOrder)
        {
            try
            {
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdString))
                {
                    return ApiResponse.UnAuthorized("User Id is misisng from token");
                }
                if (!Guid.TryParse(userIdString, out Guid userId))
                {
                    return ApiResponse.BadRequest("Invalid User Id");
                }
                var updateToOrder = await _orderServices.UpdateOrdertAsync(orderId, updateOrder, userId);
                if (updateToOrder == null)
                {
                    return ApiResponse.NotFound("Order was not found");
                }
                return ApiResponse.Success(updateToOrder, "Order updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

     
    }
}