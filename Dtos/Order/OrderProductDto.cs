using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.EntityFramework;

namespace Backend.Dtos
{
    public class OrderProductDto
    {
            public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }

    // public Order? Order { get; set; }
    // public Product? Product { get; set; }

    // public Order? Order { get; set; }
    // public Product? Product { get; set; }
    
    }
}