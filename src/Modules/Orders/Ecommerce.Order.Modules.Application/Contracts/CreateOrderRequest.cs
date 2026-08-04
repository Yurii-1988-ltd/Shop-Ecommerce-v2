using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Order.Modules.Application.Contracts;

public sealed record CreateOrderRequest(
    Guid CustomerId,
    OrderAddressDto ShippingAddress,
    List<CreateOrderItemDto> Items,
    string Currency);
