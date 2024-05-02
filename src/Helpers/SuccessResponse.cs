using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace api.Controllers
{
  public class SuccessResponse<T>
  {
    public bool Success { get; set; } = true;
    public string? Message { get; set; }
    public T? Data { get; set; }
  }

}
