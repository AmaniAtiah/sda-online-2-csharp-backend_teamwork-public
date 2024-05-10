using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Dtos
{
    public class CreateUserDto
    {

    [Required(ErrorMessage = "User Name is required")]
    [MaxLength(32,ErrorMessage = "User Name must be less than 32 character")]
    [MinLength(2,ErrorMessage = "User Name must be at least 2 character")]
    public required string  UserName { get; set; }
    [Required(ErrorMessage = "First Name is required")]
    [MaxLength(32,ErrorMessage = "First Name must be less than 32 character")]
    [MinLength(2,ErrorMessage = "First Name must be at least 2 character")]
    public required string  FirstName { get; set; }
    [Required(ErrorMessage = "Last Name is required")]
    [MaxLength(32,ErrorMessage = "Last Name must be less than 32 character")]
    [MinLength(2,ErrorMessage = "Last Name must be at least 2 character")]
    public required string  LastName { get; set; }
    [EmailAddress(ErrorMessage = "User Email Address is not valid")]
    public required string Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [MinLength(8,ErrorMessage = "Password must be at least 8 character")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,20}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one digit")]
    public required string Password { get; set; }
    [Required(ErrorMessage = "Phone Number is required")]
    [RegularExpression(@"^\+\d{10,15}$", ErrorMessage = "Invalid phone number format. Example: +1234567890")]
    public required string PhoneNumber { get; set; }
    public bool IsAdmin { get; set; }
        
    }
}