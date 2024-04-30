
public class Order {
    public Guid OrderId { get; set; }
    public required Guid UserId { get; set; }
    public User? user { get; set; } 
    public required decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
