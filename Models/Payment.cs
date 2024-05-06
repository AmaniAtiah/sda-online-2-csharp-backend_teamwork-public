namespace Backend.Models
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public required Guid OrderId { get; set; }
        public required decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public required string PaymentMethod { get; set; }
        public bool PaymentStatus { get; set; } = false;
    }
}