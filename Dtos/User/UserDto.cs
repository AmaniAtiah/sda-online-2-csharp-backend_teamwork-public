using Backend.Models;

namespace Backend.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public required string  UserName { get; set; }
        public required string  FirstName { get; set; }
        public required string  LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<Address>? Addresses { get; set; } 
        public virtual ICollection<Order>? Orders { get; set; } 
    }
}