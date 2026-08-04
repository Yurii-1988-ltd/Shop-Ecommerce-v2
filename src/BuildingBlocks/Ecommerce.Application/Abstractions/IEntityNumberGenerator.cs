using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Abstractions
{
    public interface IEntityNumberGenerator
    {
        Task<string> GenerateAsync(
        string prefix,
        CancellationToken cancellationToken = default);
    }
}
