using Ecommerce.Admin.ApiClients.Orders.Enums;

namespace Ecommerce.Admin.ApiClients.Orders.Contracts;

[property: JsonConverter(typeof(JsonStringEnumConverter))]
public record ChangeStatusRequest(OrderStatus Status);

