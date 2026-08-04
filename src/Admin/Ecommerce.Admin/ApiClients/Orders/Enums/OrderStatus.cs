using System.Text.Json.Serialization;

namespace Ecommerce.Admin.ApiClients.Orders.Enums;


[JsonConverter(typeof(JsonStringEnumConverter))]

public enum OrderStatus
{
    Pending = 1,
    Processing = 2,
    Paid = 3,
    Shipped = 4,
    Delivered = 5,
    Cancelled = 6,
    Refunded = 7,
    Draft = 8,
}
