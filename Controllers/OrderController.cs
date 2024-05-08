using Backend.EntityFramework;
using Backend.Helpers;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]//api controllers
    [Route("/api/orders")] // for httpget
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderServices;
        public OrderController(AppDbContext appDbContext)
        {
            _orderServices = new OrderService(appDbContext);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrdersControllers()
        {
            try
            {
                var orders = await _orderServices.GetAllOrdersAsync();
                if (orders.ToList().Count < 1)
                {
                    return ApiResponse.NotFound("No order found");
                }
                return ApiResponse.Success(orders, "All order are returned");
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }
        [HttpGet("{orderId:guid}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            try
            {
                var order = await _orderServices.GetOrderAsync(orderId);
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
        [HttpPost]
        public async Task<IActionResult> AddOrder(Order newOrder)
        {
            try
            {
                var createdOrder = await _orderServices.AddOrderAsync(newOrder);
                return ApiResponse.Created(createdOrder);
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

        [HttpPut("{orderId:guid}")]
        public async Task<IActionResult> UpdateOrder(Guid orderId, Order updateOrder)
        {
            try
            {
                var updateToOrder = await _orderServices.UpdateOrdertAsync(orderId, updateOrder);
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

        [HttpDelete("{orderId:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            try
            {
                var result = await _orderServices.DeleteOrderAsync(orderId);
                if (!result)
                {
                    return ApiResponse.NotFound("Order was not found");
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
