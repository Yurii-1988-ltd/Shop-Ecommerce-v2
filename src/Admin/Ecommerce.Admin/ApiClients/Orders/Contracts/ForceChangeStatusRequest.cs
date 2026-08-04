using Ecommerce.Admin.ApiClients.Orders.Enums;
using System.Text.Json.Serialization;

namespace Ecommerce.Admin.ApiClients.Orders.Contracts;

public sealed record ForceChangeStatusRequest(
[property: JsonConverter(typeof(JsonStringEnumConverter))]
OrderStatus Status,
string Reason
);
