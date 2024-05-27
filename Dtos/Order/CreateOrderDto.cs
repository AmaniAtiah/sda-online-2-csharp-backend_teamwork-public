using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Dtos
{
    public class CreateOrderDto
    {
        //  public Guid UserId { get; set; }
    public List<Guid> ProductIds { get; set; }  = new List<Guid>();
    }
}