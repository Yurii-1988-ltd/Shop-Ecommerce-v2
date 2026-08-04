
using Ecommerce.Domain.Domain;
using MediatR;

namespace Ecommerce.Application.CQRS;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;

