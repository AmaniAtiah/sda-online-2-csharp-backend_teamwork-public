using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Helpers;
using Backend.EntityFramework;
using Backend.Dtos;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Backend.Dtos.Pagination;

namespace Backend.Services
{
    public class OrderService
    {
        // List<Order> orders = new List<Order>();
        private readonly AppDbContext _appDbContext;
         private readonly IMapper _mapper;
        public OrderService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            
        }
        // public async Task<IEnumerable<OrderDtos>> GetAllOrdersAsync()

        // {
        //     try
        //     {
        //         //return await _appDbContext.Orders.ToListAsync();
        //         var orders = await _appDbContext.Orders
        //        .Select(order => new OrderDtos
        //        {
        //            OrderId = order.OrderId,
        //            OrderDate = order.OrderDate,
        //            TotalPrice = order.TotalPrice,
        //            Status = order.Status,
        //            UserId = order.UserId
        //        })
        //        .ToListAsync();
        //         return orders;
        //     }

        //     catch (Exception e)
        //     {
        //         throw new ApplicationException("An error occurred while retrieving order.", e);
        //     }
        // }
        // public async Task<Order?> GetOrderAsync(Guid OrderId, Guid userId)
        // {
        //     try
        //     {
        //         return await _appDbContext.Orders.FirstOrDefaultAsync(address => address.OrderId == OrderId && address.UserId == userId);

        //     }
        //     catch (Exception e)
        //     {
        //         throw new ApplicationException("An error occurred while retrieving order.", e);
        //     }

        // }

        // // public async Task<Order?> ShowAddressByAdmin(Guid OrderId)
        // // {
        // //     try
        // //     {
        // //         return await _appDbContext.Orders.FirstOrDefaultAsync(address => address.OrderId == OrderId);

        // //     }
        // //     catch (Exception e)
        // //     {
        // //         throw new ApplicationException("An error occurred while retrieving order.", e);
        // //     }

        // // }
        // public async Task<Order> AddOrderAsync(Order newOrder, Guid userId)
        // {

        //     try
        //     {
               
        //        // user
        //         var user = await _appDbContext.Users.FindAsync(userId);
        //         if (user == null)
        //         {
        //             throw new Exception("User not found");
        //         }
               
                
        //         Order order = new Order
        //         {
        //             OrderId = Guid.NewGuid(),
        //             OrderDate = DateTime.UtcNow,
        //             TotalPrice = newOrder.TotalPrice,
        //             Status = newOrder.Status,
        //             UserId = userId,
        //             AddressId = newOrder.AddressId
        //         };
        //         await _appDbContext.Orders.AddAsync(order);
        //         await _appDbContext.SaveChangesAsync();
        //         return order;
        //     }
        //     catch (Exception e)
        //     {
        //         throw new ApplicationException("An error occurred while adding order.", e);
        //     }

        // }

        // public async Task AddProductToOrder(Guid orderId, Guid productId, Guid userId)
        // {


        //     var order = await _appDbContext.Orders.Include(o => o.Products).FirstOrDefaultAsync(o => o.OrderId == orderId && o.UserId == userId);
        //     var product = await _appDbContext.Products.FindAsync(productId);
        //     if (order != null && product != null)
        //     {
        //         order.Products.Add(product);
        //         await _appDbContext.SaveChangesAsync();
        //     }
        //     else
        //     {
        //         throw new InvalidOperationException("The Product has already added");
        //     }
        // }


   



        // public async Task<Order?> UpdateOrdertAsync(Guid orderId, Order updateOrder, Guid userId)
        // {
        //     try
        //     {

        //         var existingOrder = await _appDbContext.Orders.FirstOrDefaultAsync(order => order.OrderId == orderId && order.UserId == userId);
        //         if (existingOrder != null)
        //         {
        //             existingOrder.OrderDate = updateOrder.OrderDate;
        //             existingOrder.TotalPrice = updateOrder.TotalPrice ?? existingOrder.TotalPrice;
        //             existingOrder.Status = updateOrder.Status ?? existingOrder.Status;
        //             existingOrder.UserId = updateOrder.UserId;
        //             await _appDbContext.SaveChangesAsync();
        //             return existingOrder;
        //         }
        //         throw new Exception("Order not found");
        //     }
        //     catch (Exception e)
        //     {
        //         throw new ApplicationException("An error occurred while updating order.", e);

        //     }
        // }
        // public async Task<bool> DeleteOrderAsync(Guid orderId, Guid userId)
        // {
        //     try
        //     {
        //         var orderToRemove = await _appDbContext.Orders.FirstOrDefaultAsync(order => order.OrderId == orderId && order.UserId == userId);
        //         if (orderToRemove != null)
        //         {
        //             _appDbContext.Orders.Remove(orderToRemove);
        //             await _appDbContext.SaveChangesAsync();
        //             return true;
        //         }
        //         throw new Exception("Order not found");
        //     }
        //     catch (Exception e)
        //     {
        //         throw new ApplicationException("An error occurred while deleting order.", e);

        //     }
        // }

        // display all orders 
             public async Task<PaginationResult<OrderDto>> GetAllOrdersAsync(int pageNumber, int pageSize)
        {

            var totalOrder = await _appDbContext.Orders
            .CountAsync();

            var orders = await _appDbContext.Orders
            .Skip((pageNumber - 1) * pageSize)            
            .Include(order => order.User)
            .Include(order => order.OrderProducts)
            .ThenInclude(orderProduct => orderProduct.Product)
    
            .Take(pageSize)
            .ToListAsync();



            var orderDtos = _mapper.Map<List<OrderDto>>(orders);

    
            return new PaginationResult<OrderDto>
            {
                Items = orderDtos,
                TotalCount = totalOrder,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

        }

        // get order by id 
        public async Task<OrderDto> GetOrderAsync(Guid orderId)
        {
            
            try
            {
                var order = await _appDbContext.Orders
               .Include(order => order.User)
               .Include(order => order.OrderProducts)

               .ThenInclude(orderProduct => orderProduct.Product)
               .FirstOrDefaultAsync(order => order.OrderId == orderId );
                if (order!= null)
                {
                    var orderDto = _mapper.Map<OrderDto>(order);
                    return orderDto;
                }
                throw new Exception("Order not found");
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while retrieving order.", e);
            }
        }



        public async Task<OrderDto> CreateOrderAsync(Guid userId, CreateOrderDto createOrderDto)
    {
        // Validate createOrderDto and perform necessary checks

        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            OrderDate = DateTime.UtcNow,
            TotalPrice = 0, // You may need to calculate the total price based on products
            Status = OrderStatus.Delivered, // Set default status or get it from DTO
            UserId = userId
        };

        // Add order products to the order
        foreach (var productId in createOrderDto.ProductIds)
        {
            var product = await _appDbContext.Products.FindAsync(productId);
            if (product != null)
            {
                var orderProduct = new OrderProduct
                {
                    OrderId = order.OrderId,
                    ProductId = productId,
                    // Set other order product properties
                };
                _appDbContext.OrderProducts.Add(orderProduct);
                order.TotalPrice += product.Price; // Add product price to total price
            }
            else
            {
                throw new ArgumentException($"Product with ID {productId} not found");
            }
        }

        // Save the order to the database
        _appDbContext.Orders.Add(order);
        await _appDbContext.SaveChangesAsync();

        // Map the created order entity to a DTO and return it
        var orderDto = _mapper.Map<OrderDto>(order);
        return orderDto;
    }
}
    }
