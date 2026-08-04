

using System.Data;

namespace Ecommerce.Application.Abstractions;

public interface IDbConnectionFactory
{
    ValueTask<IDbConnection> OpenConnectionAsync();
}
