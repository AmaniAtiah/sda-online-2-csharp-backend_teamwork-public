using System.Runtime.InteropServices;
using System.Security.Claims;
using Backend.Dtos;
using Backend.EntityFramework;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]//api controllers
    [Route("/api/orders")] // for httpget
    // admin can show all orders and update status of order
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderServices;
        public OrderController(OrderService orderService)
        {
            _orderServices = orderService;
        }

        // [Authorize(Roles = "Admin")]
        // [HttpGet]
        // public async Task<IActionResult> GetAllOrders()
        // {
        //     try
        //     {
        //         var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
        //         if (!isAdmin)
        //         {
        //             return ApiResponse.Forbidden("Only admin can visit this route");
        //         }
        //         var orders = await _orderServices.GetAllOrdersAsync();
        //         if (orders.ToList().Count < 1)
        //         {
        //             return ApiResponse.NotFound("No order found");
        //         }
        //         return ApiResponse.Success(orders, "all orders retrieved successfully");
        //     }
        //     catch (Exception ex)
        //     {
        //         return ApiResponse.ServerError(ex.Message);
        //     }
        // }

        // [Authorize]
        // [HttpGet("{orderId:guid}")]
        // public async Task<IActionResult> GetOrderById(Guid orderId)
        // {
        //     try
        //     {
        //         var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //         if (string.IsNullOrEmpty(userIdString))
        //         {
        //             return ApiResponse.UnAuthorized("User Id is misisng from token");
        //         }
        //         if (!Guid.TryParse(userIdString, out Guid userId))
        //         {
        //             return ApiResponse.BadRequest("Invalid User Id");
        //         }
                 

        //         var order = await _orderServices.GetOrderAsync(orderId, userId);
        //         if (order == null)
        //         {
        //             return ApiResponse.NotFound("Order was not found");
        //         }
        //         return ApiResponse.Created(order);
        //     }
        //     catch (Exception ex)
        //     {
        //         return ApiResponse.ServerError(ex.Message);
        //     }
        // }


        // [Authorize]
        // [HttpPost]
        // public async Task<IActionResult> AddOrder(Order newOrder, Guid userId)//
        // {
        //     try
        //     {
        //         var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //         if (string.IsNullOrEmpty(userIdString))
        //         {
        //             return ApiResponse.UnAuthorized("User Id is misisng from token");
        //         }
        //         if (!Guid.TryParse(userIdString, out userId))
        //         {
        //             return ApiResponse.BadRequest("Invalid User Id");
        //         }
                 
                
        //         var createdOrder = await _orderServices.AddOrderAsync(newOrder, userId);//
        //         return ApiResponse.Created(createdOrder);
        //     }
        //     catch (Exception ex)
        //     {
        //         return ApiResponse.ServerError(ex.Message);
        //     }
        // }

        // [Authorize]
        // [HttpPost("{orderId:guid}")]
        // public async Task<IActionResult> AddProductToOrder(Guid orderId, Guid productId, Guid userId)//
        // {
        //     try
        //     {
        //           var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //         if (string.IsNullOrEmpty(userIdString))
        //         {
        //             return ApiResponse.UnAuthorized("User Id is misisng from token");
        //         }
        //         if (!Guid.TryParse(userIdString, out userId))
        //         {
        //             return ApiResponse.BadRequest("Invalid User Id");
        //         }
        //         await _orderServices.AddProductToOrder(orderId, productId, userId);//
        //         return ApiResponse.Created("Products Added to the order successfully");
        //     }
        //     catch (Exception e)
        //     {
        //         return ApiResponse.ServerError(e.Message);
        //     }
        // }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 3)
        {
            var users = await _orderServices.GetAllOrdersAsync(pageNumber, pageSize);

        
            return ApiResponse.Success(users, "All orders are returned successfully");
        }

        // get order 
        [Authorize]
        [HttpGet("{orderId:guid}")]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            try
            {
                var order = await _orderServices.GetOrderAsync(orderId);
                if (order!= null)
                {
                    return ApiResponse.Success(order, "Order is retrieved successfully");
                }
                else
                {
                    return ApiResponse.NotFound("Order was not found");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

        
    [Authorize]
        [HttpPost("{userId}/add-order")]
public async Task<IActionResult> CreateOrder(Guid userId, [FromBody] CreateOrderDto createOrderDto)
    {
        try
        {
            var orderDto = await _orderServices.CreateOrderAsync(userId, createOrderDto);
            return Ok(orderDto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the order: {ex.Message}");
        }
    }
  


     
    }
}