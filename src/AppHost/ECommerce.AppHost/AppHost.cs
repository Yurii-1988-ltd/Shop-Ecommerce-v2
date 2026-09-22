var builder = DistributedApplication.CreateBuilder(args);

// 1. Инфраструктура БД
var sql = builder.AddSqlServer("sql")
    .WithDataVolume();

var database = sql.AddDatabase("Ecommerce");

// RabbitMq 
var messaging = builder.AddRabbitMQ("messaging")
    .WithManagementPlugin()
    .WithDataVolume()
    .WithEnvironment(
        "RABBITMQ_SERVER_ADDITIONAL_ERL_ARGS",
        "-rabbit loopback_users []");

//mailpit
var mailpit = builder.AddContainer("mailpit", "axllent/mailpit")
    .WithHttpEndpoint(port: 8025, targetPort: 8025, name: "dashboard")
    .WithEndpoint(port: 1025, targetPort: 1025, name: "smtp");

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
var notification = postgres.AddDatabase("notifications");



var mongo = builder.AddMongoDB("mongo")
    .WithDataVolume();

var catalog = mongo.AddDatabase("Catalog");
var cart = mongo.AddDatabase("Cart");
var order = mongo.AddDatabase("Order");
var coupon = mongo.AddDatabase("Coupon");


// 2. Сервисы
var api = builder.AddProject<Projects.Ecommerce_API>("ecommerce-api")
    .WithReference(database)
    .WithReference(catalog)
    .WithReference(cart)
    .WithReference(order)
    .WithReference(inventory)
    .WithReference(notification)
    .WithReference(coupon)
    .WithReference(messaging);

builder.AddProject<Projects.Ecommerce_Admin>("ecommerce-admin")
    .WithReference(api);

builder.AddProject<Projects.Ecommerce_Storefront>("ecommerce-storefront");

builder.Build().Run();