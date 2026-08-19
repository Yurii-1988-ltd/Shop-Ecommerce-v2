var builder = DistributedApplication.CreateBuilder(args);

// 1. Инфраструктура БД
var sql = builder.AddSqlServer("sql")
    .WithDataVolume();

var database = sql.AddDatabase("Ecommerce");

// Создаем ресурс Postgres с явным указанием переменной окружения POSTGRES_PASSWORD
var postgres = builder.AddPostgres("postgres")
    .WithEnvironment(
        "POSTGRES_PASSWORD",
        "postgres")
    //.WithDataVolume()
    .WithEndpoint(
        port: 5432,
        targetPort: 5432,
        name: "tcp");
var inventory = postgres.AddDatabase("inventories");

var mongo = builder.AddMongoDB("mongo")
    .WithDataVolume();

var catalog = mongo.AddDatabase("Catalog");
var cart = mongo.AddDatabase("Cart");
var order = mongo.AddDatabase("Order");


// 2. Сервисы
var api = builder.AddProject<Projects.Ecommerce_API>("ecommerce-api")
    .WithReference(database)
    .WithReference(catalog)
    .WithReference(cart)
    .WithReference(order)
    .WithReference(inventory) ;

builder.AddProject<Projects.Ecommerce_Admin>("ecommerce-admin")
    .WithReference(api);

builder.Build().Run();