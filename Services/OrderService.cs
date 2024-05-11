using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.EntityFramework;
using Backend.Dtos;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<Order?> GetOrderAsync(Guid OrderId)
        {
            return await _appDbContext.Orders.FindAsync(OrderId);

        }
        public async Task<Order> AddOrderAsync(Order newOrder)
        {
            Order order = new Order
            {
                OrderId = Guid.NewGuid(),
                OrderDate = DateTime.UtcNow,
                TotalPrice = newOrder.TotalPrice,
                Status = newOrder.Status,
                UserId = newOrder.UserId,
                AddressId = newOrder.AddressId
            };
            await _appDbContext.Orders.AddAsync(order);
            await _appDbContext.SaveChangesAsync();
            return order;
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
                throw new InvalidOperationException("The Product Id or order Id is Not vaild");
            }
        }
        public async Task<Order?> UpdateOrdertAsync(Guid orderId, OrderDtos updateOrder)
        {
            var existingOrder = await _appDbContext.Orders.FindAsync(orderId);
            if (existingOrder != null)
            {
                existingOrder.OrderDate = existingOrder.OrderDate;
                existingOrder.TotalPrice = updateOrder.TotalPrice ?? existingOrder.TotalPrice;
                existingOrder.Status = updateOrder.Status.IsNullOrEmpty() ? existingOrder.Status : updateOrder.Status;
                existingOrder.UserId = existingOrder.UserId;
                await _appDbContext.SaveChangesAsync();
                return existingOrder;
            }
            throw new Exception("Order not found");
        }
        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var orderToRemove = await _appDbContext.Orders.FindAsync(orderId);
            if (orderToRemove != null)
            {
                _appDbContext.Orders.Remove(orderToRemove);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}