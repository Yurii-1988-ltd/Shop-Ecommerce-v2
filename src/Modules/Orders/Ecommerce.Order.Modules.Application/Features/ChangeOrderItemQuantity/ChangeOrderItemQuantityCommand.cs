

namespace Ecommerce.Order.Modules.Application.Features.ChangeOrderItemQuantity;

public sealed record ChangeOrderItemQuantityCommand(Guid OrderId,
                                                    Guid OrderItemId,
                                                    int Quantity) : ICommand;
 

