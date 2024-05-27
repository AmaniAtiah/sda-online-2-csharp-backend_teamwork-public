using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.EntityFramework
{
    [Table("Users")]
    public class User
    {
        public Guid UserId { get; set; }
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PhoneNumber { get; set; }
        // address not required 
        public string Address { get; set; } = string.Empty;

        public bool IsAdmin { get; set; }
        public bool IsBanned { get; set; } 

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        // public virtual ICollection<Address>? Addresses { get; set; } 
        public virtual ICollection<Order>? Orders { get; set; } 
 
        public  Cart? Cart { get; set; }

    }
}