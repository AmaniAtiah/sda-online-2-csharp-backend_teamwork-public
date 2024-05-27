// using System;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace Backend.EntityFramework
// {
//     [Table("Address")]
//     public class AddressTable
//     {
//         [Key, Required]
//         public Guid AddressId { get; set; }

//         [Required]
//         public string? AddressLine { get; set; }

//         [Required]
//         public string? City { get; set; }

//         [Required]
//         public string? State { get; set; }

//         [Required]
//         public string? Country { get; set; }

//         [Required]
//         public string? ZipCode { get; set; }

//         [ForeignKey("User")]
//         public Guid UserId { get; set; }
//     }
// }
