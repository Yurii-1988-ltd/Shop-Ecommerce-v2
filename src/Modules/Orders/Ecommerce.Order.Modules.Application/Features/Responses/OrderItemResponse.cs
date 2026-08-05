using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Order.Modules.Application.Features.Responses;

public sealed record OrderItemResponse(Guid Id,
    Guid ProductId,
    string ProductName,
    string SKU,
    decimal Amount,
    string Currency,
    int Quantity,
    decimal TotalPrice);

