using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.EntityFramework
{
    public class OrderProduct
    {
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
 

    public Order? Order { get; set; }
    public Product? Product { get; set; }
        
    }
}