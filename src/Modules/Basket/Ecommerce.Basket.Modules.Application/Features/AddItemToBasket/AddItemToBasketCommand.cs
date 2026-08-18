

using Ecommerce.Application.CQRS;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Basket.Modules.Application.Features.AddItemToBasket;

public sealed record AddItemToBasketCommand(Guid CustomerId,
                                            Guid ProductId,
                                            string ProductName,
                                            Money UnitPrice,
                                            int Quantity) : ICommand<Guid>;
