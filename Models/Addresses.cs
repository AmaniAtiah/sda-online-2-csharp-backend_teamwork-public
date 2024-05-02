public class Address

{
    public Guid AddressId { get; set; }
    public required string AddressLine { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string Country { get; set; }
    public required string ZipCode { get; set; }
    public Guid UserId { get; set; }
}
