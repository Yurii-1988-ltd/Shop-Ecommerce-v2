

using Ecommerce.Domain.Domain;
using MediatR;

namespace Ecommerce.Application.CQRS;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;

