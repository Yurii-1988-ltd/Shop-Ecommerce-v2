

using Ecommerce.Domain.Domain;
using MediatR;

namespace Ecommerce.Application.CQRS;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;

