using Ecommerce.Order.Modules.Application.Features.Responses;
using Ecommerce.Order.Modules.Domain.Entities;
using Ecommerce.Order.Modules.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Order.Modules.Application.Mappings
{
    internal static class OrderAddressMapping
    {
        public static AddressResponse ToResponse(this OrderAddress orderAddress)
        {
            return new AddressResponse(
                orderAddress.FirstName, 
                orderAddress.LastName,
                orderAddress.Country,
                orderAddress.City,
                orderAddress.Street,
                orderAddress.ZipCode);

        }
    }
}
