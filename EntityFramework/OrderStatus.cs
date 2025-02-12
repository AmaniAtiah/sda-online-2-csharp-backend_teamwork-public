using System.Runtime.Serialization;

namespace Backend.EntityFramework
{
    public enum OrderStatus
    {
   [EnumMember(Value = "Pending")]
    Pending,
    [EnumMember(Value = "Processing")]
    Processing,
    [EnumMember(Value = "Shipped")]
    Shipped,
    [EnumMember(Value = "Delivered")]
    Delivered,
    [EnumMember(Value = "Cancelled")]
    Cancelled
        
    }
}