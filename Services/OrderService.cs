using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Helpers;
using Backend.Models;
using Backend.EntityFramework;

namespace Backend.Services
{
    public class OrderService
    {
        List<Order> orders = new List<Order>();
        private readonly AppDbContext _appDbContext;
<<<<<<< HEAD
        public OrderService(AppDbContext appDpContext)
        {
            _appDbContext = appDpContext;
=======
        public OrderService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
>>>>>>> a2f2879185d485590f8e73d13c7aded13d24c182
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
<<<<<<< HEAD
                return await _appDbContext.Orders
                 .Select(o => new Order
                 {
                     OrderId = o.OrderId,
                     OrderDate = o.OrderDate,
                     TotalPrice = o.TotalPrice,
                     Status = o.Status,
                     UserId = o.UserId
                 })
                .ToListAsync();
=======
                return await _appDbContext.Orders.Include(p => p.Products).ToListAsync();
>>>>>>> a2f2879185d485590f8e73d13c7aded13d24c182
            }

            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while retrieving order.", e);
            }
        }
        public async Task<Order?> GetOrderAsync(Guid ProductId)
        {
            try
            {
                return await _appDbContext.Orders.FindAsync(ProductId);
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while retrieving order.", e);
            }

        }
        public async Task<Order> AddOrderAsync(Order newOrder)
        {
            try
            {
                Order order = new Order
                {
                    OrderId = Guid.NewGuid(),
                    OrderDate = DateTime.UtcNow,
                    TotalPrice = newOrder.TotalPrice,
                    Status = newOrder.Status,
                    UserId = newOrder.UserId
                    //AddresseId = newOrder.AddresseId
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
        public async Task AddProductToOrder(Guid orderId, Guid productId)
        {
            var order = await _appDbContext.Orders.Include(o => o.Products).FirstOrDefaultAsync(o => o.OrderId == orderId);
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
<<<<<<< HEAD
=======

>>>>>>> a2f2879185d485590f8e73d13c7aded13d24c182
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