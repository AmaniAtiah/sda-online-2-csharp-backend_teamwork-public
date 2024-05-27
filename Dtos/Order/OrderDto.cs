using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.EntityFramework;

namespace Backend.Dtos
{
    public class OrderDto
    {
            public Guid OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
     public Guid UserId { get; set; }
     public User? User { get; set; }
    // Other order properties

    public List<OrderProductDto> OrderProducts { get; set; } = new List<OrderProductDto>();
}
        
}