using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Helpers;
using Backend.Models;
using Backend.EntityFramework;
using Backend.Dtos;

namespace Backend.Services
{
    public class OrderService
    {
        List<Order> orders = new List<Order>();
        private readonly AppDbContext _appDbContext;
        public OrderService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<OrderDtos>> GetAllOrdersAsync()
        {
            try
            {
                //return await _appDbContext.Orders.ToListAsync();
                var orders = await _appDbContext.Orders
               .Select(order => new OrderDtos
               {
                   OrderId = order.OrderId,
                   OrderDate = order.OrderDate,
                   TotalPrice = order.TotalPrice,
                   Status = order.Status,
                   UserId = order.UserId
               })
               .ToListAsync();
                return orders;
            }

            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while retrieving order.", e);
            }
        }
        public async Task<Order?> GetOrderAsync(Guid OrderId, Guid userId)
        {
            try
            {
                return await _appDbContext.Orders.FirstOrDefaultAsync(address => address.OrderId == OrderId && address.UserId == userId);

            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while retrieving order.", e);
            }

        }
        public async Task<Order> AddOrderAsync(Order newOrder, Guid userId)
        {

            try
            {
                var address = await _appDbContext.Addresses.FirstOrDefaultAsync(a => a.AddressId == newOrder.AddressId && a.UserId == userId);

                if (address == null)
                {
                    throw new UnauthorizedAccessException("User is not authorized to use this address.");
                }
                Order order = new Order
                {
                    OrderId = Guid.NewGuid(),
                    OrderDate = DateTime.UtcNow,
                    TotalPrice = newOrder.TotalPrice,
                    Status = newOrder.Status,
                    UserId = userId,
                    AddressId = newOrder.AddressId
                };
                await _appDbContext.Orders.AddAsync(order);
                await _appDbContext.SaveChangesAsync();
                return order;
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while adding order.", e);
            }

        }

        public async Task AddProductToOrder(Guid orderId, Guid productId, Guid userId)
        {


            var order = await _appDbContext.Orders.Include(o => o.Products).FirstOrDefaultAsync(o => o.OrderId == orderId &&  o.UserId == userId);
            var product = await _appDbContext.Products.FindAsync(productId);
            if (order != null && product != null)
            {
                order.Products.Add(product);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("The Product has already added");
            }
        }


 

        public async Task<Order?> UpdateOrdertAsync(Guid orderId, Order updateOrder)
        {
            try
            {
                var existingOrder = await _appDbContext.Orders.FindAsync(orderId);
                if (existingOrder != null)
                {
                    existingOrder.OrderDate = updateOrder.OrderDate;
                    existingOrder.TotalPrice = updateOrder.TotalPrice ?? existingOrder.TotalPrice;
                    existingOrder.Status = updateOrder.Status ?? existingOrder.Status;
                    existingOrder.UserId = updateOrder.UserId;
                    await _appDbContext.SaveChangesAsync();
                    return existingOrder;
                }
                throw new Exception("Order not found");
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while updating order.", e);

            }
        }
        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            try
            {
                var orderToRemove = await _appDbContext.Orders.FindAsync(orderId);
                if (orderToRemove != null)
                {
                    _appDbContext.Orders.Remove(orderToRemove);
                    await _appDbContext.SaveChangesAsync();
                    return true;
                }
                throw new Exception("Order not found");
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while deleting order.", e);

            }
        }
    }
}