using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.EntityFramework;

namespace Backend.Dtos
{

    public class CartDto
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        // cartproduct many to many 
       public List<CartProduct>? CartProducts { get; set; }
        // date 
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        

    }
}