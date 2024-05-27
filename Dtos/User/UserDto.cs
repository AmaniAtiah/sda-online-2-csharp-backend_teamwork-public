
using Backend.EntityFramework;

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
        public string Address { get; set; } = string.Empty;

        public bool IsAdmin { get; set; }
        public bool IsBanned { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        // public virtual ICollection<Address>? Addresses { get; set; } 
        public virtual ICollection<OrderDto>? Orders { get; set; } 
        // user has one cart
        // public  Guid CartId { get; set; }
        public CartDto? Cart { get; set; }
    }
}