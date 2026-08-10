using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Inventory.Modules.Application.Responses;

    public sealed record InventoryReportItem(
    Guid ProductId,
    string SKU,
    int OnHandQuantity,
    int ReservedQuantity,
    int AvailableQuantity,
    int MinimumQuantity);
