public class User {
    public Guid UserId { get; set; }
    public required string  Username { get; set; }
    public required string  FirstName { get; set; }
    public required string  LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PhoneNumber { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}

