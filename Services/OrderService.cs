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
        private readonly AppDbContext _dbContext;
        public OrderService(AppDbContext appcontext)
        {
            _dbContext = appcontext;
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                return await _dbContext.Orders.ToArrayAsync();
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
                return await _dbContext.Orders.FindAsync(ProductId);
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
                _dbContext.Orders.Add(newOrder);
                await _dbContext.SaveChangesAsync();
                return newOrder;
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while adding order.", e);
            }

        }
        public async Task<Order?> UpdateOrdertAsync(Guid orderId, Order updateOrder)
        {
            try
            {
                var existingOrder = await _dbContext.Orders.FindAsync(orderId);
                if (existingOrder != null)
                {
                    //existingOrder.OrderDate = updateOrder.OrderDate ?? existingOrder.OrderDate;
                    existingOrder.TotalPrice = updateOrder.TotalPrice ?? existingOrder.TotalPrice;
                    existingOrder.Status = updateOrder.Status ?? existingOrder.Status;

                    await _dbContext.SaveChangesAsync();
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
                var orderToRemove = await _dbContext.Orders.FindAsync(orderId);
                if (orderToRemove != null)
                {
                    _dbContext.Orders.Remove(orderToRemove);
                    await _dbContext.SaveChangesAsync();
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