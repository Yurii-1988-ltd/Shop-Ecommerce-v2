namespace Ecommerce.Application.Mappings;


using Ecommerce.Domain.ValueObjects;
using MongoDB.Bson.Serialization;

public static class MoneyMapping
{
    public static void Register()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(Money)))
            return;

        BsonClassMap.RegisterClassMap<Money>(cm =>
        {
            cm.AutoMap();

            // Явно сопоставляем BSON-элементы с аргументами конструктора
            cm.MapConstructor(typeof(Money).GetConstructor(new[] { typeof(decimal), typeof(string) })!)
              .SetArguments(new[] { "Amount", "Currency" }); // Или "amount", "currency", если в базе имена с маленькой буквы
        });
    }
}