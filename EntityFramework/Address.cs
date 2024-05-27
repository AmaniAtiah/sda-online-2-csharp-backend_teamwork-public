// using System.ComponentModel.DataAnnotations.Schema;
// using System.ComponentModel.DataAnnotations;

// namespace Backend.EntityFramework
// {
//     [Table("Address")]
//     public class Address
//     {
//         public Guid AddressId { get; set; }

//         [Required(ErrorMessage = "Address line is required.")]
//         [MaxLength(255, ErrorMessage = "Address line cannot exceed 255 characters.")]
//         public string? AddressLine { get; set; }

//         [Required(ErrorMessage = "City is required.")]
//         [MaxLength(100, ErrorMessage = "City name cannot exceed 100 characters.")]
//         public string? City { get; set; }

//         [Required(ErrorMessage = "State is required.")]
//         [MaxLength(100, ErrorMessage = "State name cannot exceed 100 characters.")]
//         public string? State { get; set; }

//         [Required(ErrorMessage = "Country is required.")]
//         [MaxLength(100, ErrorMessage = "Country name cannot exceed 100 characters.")]
//         public string? Country { get; set; }

//         [Required(ErrorMessage = "Zip code is required.")]
//         [MaxLength(20, ErrorMessage = "Zip code cannot exceed 20 characters.")]
//         public string? ZipCode { get; set; }
//         //Relations:
//         [Required(ErrorMessage = "UserId is required")]
//         public Guid UserId { get; set; } //FK
//         public User? User { get; set; }
//         public List<Order> Orders { get; set; } = new List<Order>();//1-many relation
//     }
// }