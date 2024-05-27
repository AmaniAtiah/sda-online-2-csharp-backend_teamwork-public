using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos
{
    public class UpdateUserDto
    {
        [MaxLength(32, ErrorMessage = "User Name must be less than 32 characters")]
        [MinLength(2, ErrorMessage = "User Name must be at least 2 characters")]
        public string? UserName { get; set; }

        [MaxLength(32, ErrorMessage = "First Name must be less than 32 characters")]
        [MinLength(2, ErrorMessage = "First Name must be at least 2 characters")]
        public string? FirstName { get; set; }

        [MaxLength(32, ErrorMessage = "Last Name must be less than 32 characters")]
        [MinLength(2, ErrorMessage = "Last Name must be at least 2 characters")]
        public string? LastName { get; set; } 

        [EmailAddress(ErrorMessage = "User Email Address is not valid")]
        public string? Email { get; set; } 

        // [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,20}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one digit")]
        // public string? Password { get; set; } 

        [RegularExpression(@"^\+\d{10,15}$", ErrorMessage = "Invalid phone number format. Example: +1234567890")]
        public string? PhoneNumber { get; set; } 
        // address
        public string? Address { get; set; }
        public bool? IsAdmin { get; set; }
         public bool? IsBanned { get; set; } 

    }
}