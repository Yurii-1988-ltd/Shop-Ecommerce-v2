using Ecommerce.Catalog.Modules.Application.Dto;
using Ecommerce.Domain.Errors;
using Ecommerce.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public static class MoneyMappingExtensions
{
    public static Result<Money> ToMoney(this MoneyDto dto)
    {
        if (dto is null)
            return Result.Failure<Money>(MoneyErrors.InvalidCurrency);

        return Money.Create(dto.Amount, dto.Currency);
    }
}


